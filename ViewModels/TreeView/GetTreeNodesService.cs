using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DatabaseTask.Models;
using DatabaseTask.Services.Collection;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.TreeView
{
    public partial class GetTreeNodesService : ViewModelBase, IGetTreeNodes
    {
        private readonly ITreeView _treeView;
        private List<FileProperties> _filesProperties = new List<FileProperties>();

        public SmartCollection<FileProperties> FilesProperties { get; } = new SmartCollection<FileProperties>();

        public ITreeView TreeView { get => _treeView; }


        public GetTreeNodesService(ITreeView treeView)
        {
            _treeView = treeView;
            _treeView.SelectionChanged += OnSelectionChanged;
        }

        public async Task GetCollectionFromFolders(IEnumerable<IStorageFolder> folders)
        {
            await GetCollectionByRecursion(folders);
        }

        private async Task GetCollectionByRecursion(IEnumerable<IStorageFolder> folders)
        {
            _treeView.Nodes.Clear();
            foreach (IStorageFolder folder in folders)
            {
                await ProcessFolders(folder, _treeView.Nodes);
            }
        }

        private async Task ProcessFolders(IStorageItem folder, SmartCollection<INode> collection, INode parent = null)
        {
            (StorageItemProperties settings, SmartCollection<INode> childrens) =
                 await GetData(folder, parent);
            collection.Add(GetNode(folder, settings, childrens, parent));
        }

        private async Task<(StorageItemProperties, SmartCollection<INode>)> GetData(IStorageItem item, INode parent = null)
        {
            StorageItemProperties settings = await item.GetBasicPropertiesAsync();
            SmartCollection<INode> childrens = new SmartCollection<INode>();
            if (item is IStorageFolder folder)
            {
                IAsyncEnumerator<IStorageItem> items = folder.GetItemsAsync().GetAsyncEnumerator();
                childrens = await GetChildren(items, parent);
            }
            return (settings, childrens);
        }

        private async Task<SmartCollection<INode>> GetChildren(IAsyncEnumerator<IStorageItem> items, INode parent = null)
        {

            SmartCollection<INode> children = new SmartCollection<INode>();
            bool hasMany = true;
            IStorageItem? item = null;
            while (hasMany)
            {
                try
                {
                    hasMany = await items.MoveNextAsync();
                    item = hasMany ? items.Current : null;
                    if (item == null)
                    {
                        break;
                    }
                    if (HasFlag(item, FileAttributes.Hidden))
                    {
                        continue;
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    continue;
                }
                await ProcessFolders(item, children, parent);
            }
            return children;
        }

        private bool HasFlag(IStorageItem? item, Enum flag)
        {
            FileAttributes attributes = File.GetAttributes(item.TryGetLocalPath());
            if (attributes.HasFlag(flag))
            {
                return true;
            }
            return false;
        }

        private NodeViewModel GetNode(IStorageItem item, StorageItemProperties properties,
            SmartCollection<INode> children, INode parent = null)
        {
            NodeViewModel node = new NodeViewModel()
            {
                Name = item.Name,
                IsFolder = item is IStorageFolder ? true : false,
                IconPath = item is IStorageFolder ?
                IconCategory.Folder.Value : IconCategory.File.Value,
                Parent = parent,
            };

            DateTimeOffset? modifiedTime = properties.DateModified;
            string modifiedString = "";
            if (modifiedTime != null)
            {
                DateTimeOffset time = modifiedTime.Value;
                modifiedString = time.ToString("HH:mm");
            }

            _filesProperties.Add(new FileProperties(item.Name, 
                item is IStorageFolder? "" : Math.Ceiling((double)properties.Size / 1024).ToString() + " KB",
                modifiedString,
                item is IStorageFolder ?
                IconCategory.Folder.Value : IconCategory.File.Value,
                parent));

            foreach (NodeViewModel child in children)
            {
                child.Parent = node;
            }

            node.Children.AddRange(children);
            node.Expanded += ExpandHandler;
            node.Collapsed += CollapsedHandler;
            return node;
        }

        private void ExpandHandler(INode model)
        {
            ExpandCollapsedImpl(model, IconCategory.OpenedFolder);
        }

        private void CollapsedHandler(INode model)
        {
            ExpandCollapsedImpl(model, IconCategory.Folder);
        }

        private void ExpandCollapsedImpl(INode model, IconCategory iconPath)
        {
            if (model is NodeViewModel node)
            {
                node.IconPath = iconPath.Value;
                _treeView.SelectedNodes.Clear();
                _treeView.SelectedNodes.Add(node);
            }
        }

        private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (_treeView.SelectedNodes.Count == 1)
            {
                var t = _filesProperties.Where(x => x.Parent == _treeView.SelectedNodes[0]);
                FilesProperties.AddRange(t);
            }
        }
    }
}
