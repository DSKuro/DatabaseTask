using Avalonia.Platform.Storage;
using DatabaseTask.Models;
using DatabaseTask.Services.Collection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels.TreeView.Interfaces
{
    public interface IGetTreeNodes
    {
        public ITreeView TreeView { get; }
        public SmartCollection<FileProperties> FilesProperties { get; }

        public Task GetCollectionFromFolders
            (IEnumerable<IStorageFolder> folders);
    }
}
