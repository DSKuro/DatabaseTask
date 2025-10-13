using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Operations.Utils.Interfaces
{
    public interface INameGenerator
    {
        public string GenerateUniqueName(INode parent, string baseName);
        public string GenerateUniqueCopyName(INode parent, string baseName);
    }
}
