using Avalonia.Platform.Storage;
using DatabaseTask.Services.Collection;
using DatabaseTask.ViewModels.Nodes;
using DatabaseTask.ViewModels.TreeView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class GetTreeNodesService : ViewModelBase, IGetTreeNodes
    {
        private readonly ITreeView _treeView;

        public ITreeView TreeView { get => _treeView; }

        public GetTreeNodesService(ITreeView treeView)
        {
            _treeView = treeView;
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
                (StorageItemProperties settings, SmartCollection<NodeViewModel> childrens) =
                 await GetData(folder);
                _treeView.Nodes.Add(GetNode(folder, settings, childrens));
            }
        }

        private async Task<(StorageItemProperties, SmartCollection<NodeViewModel>)> GetData(IStorageItem item)
        {
            StorageItemProperties settings = await item.GetBasicPropertiesAsync();
            SmartCollection<NodeViewModel> childrens = new SmartCollection<NodeViewModel>();
            if (item is IStorageFolder folder)
            {
                IAsyncEnumerable<IStorageItem> items = folder.GetItemsAsync();
                childrens = await GetChildren(items);
            }
            return (settings, childrens);
        }

        private async Task<SmartCollection<NodeViewModel>> GetChildren(IAsyncEnumerable<IStorageItem> items)
        {
            SmartCollection<NodeViewModel> children = new();
            await foreach (IStorageItem item in items)
            {
                (StorageItemProperties settings, SmartCollection<NodeViewModel> childrens) =
                  await GetData(item);
                children.Add(GetNode(item, settings, childrens));
            }
            return children;
        }

        private NodeViewModel GetNode(IStorageItem item, StorageItemProperties properties,
            SmartCollection<NodeViewModel> children)
        {
            NodeViewModel node = new NodeViewModel()
            {
                Name = item.Name,
                IsFolder = (item is IStorageFolder) ? true : false,
                IconPath = (item is IStorageFolder) ?
                IconCategory.Folder.Value : IconCategory.File.Value
            };
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
                _treeView.SelectedNode = node;
            }
        }
    }
}
