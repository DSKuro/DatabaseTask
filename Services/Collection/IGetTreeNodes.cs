using Avalonia.Platform.Storage;
using DatabaseTask.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Collection
{
    public interface IGetTreeNodes
    {
        public Task<SmartCollection<NodeViewModel>> GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
