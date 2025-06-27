using Avalonia.Platform.Storage;
using DatabaseTask.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Collection
{
    public class GetTreeNodesService : IGetTreeNodes
    {
        private SmartCollection<NodeViewModel> _nodes = new SmartCollection<NodeViewModel>();

        public async Task<SmartCollection<NodeViewModel>> GetCollectionFromFolders(IEnumerable<IStorageFolder> folders)
        {
            await GetCollectionByRecursion(folders);
            return _nodes;
        }

        private async Task GetCollectionByRecursion(IEnumerable<IStorageFolder> folders)
        {
            foreach (IStorageFolder folder in folders)
            {
                await ProcessData(folder, _nodes);
            }
        }

        private async Task ProcessData(IStorageItem folder, SmartCollection<NodeViewModel> collection)
        {
            (StorageItemProperties settings, SmartCollection<NodeViewModel> childrens) =
                await GetData(folder);
            collection.Add(GetNode(folder, settings, childrens));
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
                await ProcessData(item, children);
            }
            return children;
        }

        private NodeViewModel GetNode(IStorageItem item, StorageItemProperties properties,
            SmartCollection<NodeViewModel> children)
        {
            NodeViewModel node = new NodeViewModel()
            {
                Name = item.Name,
                IconPath = (item is IStorageFolder) ?
                IconCategory.Folder.Value : IconCategory.File.Value
            };
            node.Children.AddRange(children);
            return node;
        }
    }
}
