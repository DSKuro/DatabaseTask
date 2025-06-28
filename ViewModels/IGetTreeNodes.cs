using Avalonia.Platform.Storage;
using DatabaseTask.ViewModels.TreeView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public interface IGetTreeNodes
    {
        public ITreeView TreeView { get; }

        public Task GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
