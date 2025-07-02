using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.TreeView.Interfaces
{
    public interface IGetTreeNodes
    {
        public ITreeView TreeView { get; }

        public Task GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
