using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Operations.FilesOperations.Interfaces
{
    public interface IFullPath
    {
        public string? PathToCoreFolder { get; set; }

        public string GetPathForNewItem(INode node, string newItemName);
        public string GetRelativePath(INode node, string newItemName);
    }
}
