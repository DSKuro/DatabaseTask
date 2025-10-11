using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces
{
    public interface IDeleteItemOperation
    {
        public void DeleteItem(List<INode> nodes);
    }
}
