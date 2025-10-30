using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.EventArguments;
using DatabaseTask.ViewModels.MainViewModel.Controls.TreeView.Interfaces;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CreateFolderOperation : ICreateFolderOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly ITreeView _treeView;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        public CreateFolderOperation(ITreeViewFunctionality treeViewFunctionality,
            ITreeView treeView,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _treeView = treeView;
            _dataGridFunctionality = dataGridFunctionality;
        }

        public async Task CreateFolder(string folderName)
        {
            INode? parent = _treeViewFunctionality.GetFirstSelectedNode();
            if (parent != null)
            {
                await CreateFolderImplementation(folderName, parent);
            }
        }

        private async Task CreateFolderImplementation(string folderName, INode parent)
        {
            NodeViewModel node = CreateNode(folderName);
            int index = 0;
            bool isInsert = _treeViewFunctionality.TryInsertNode(parent, node, out index);
            if (isInsert)
            {
                await UpdateProperties(index, parent, node) ;
            }
        }

        private async Task UpdateProperties(int index, INode parent, INode node)
        {
            _dataGridFunctionality.TryInsertProperties(index, parent, CreateFileProperties(node));
            _treeViewFunctionality.UpdateSelectedNodes(node);
            await Task.Delay(100);
            _treeView.ScrollChanged?.Invoke(this, new TreeViewEventArgs(node));
        }

        private FileProperties CreateFileProperties(INode node)
        {
            return new FileProperties
                (
                    node.Name,
                    "",
                    _dataGridFunctionality.TimeToString(DateTime.Now),
                    IconCategory.Folder.Value, node
                );
        }

        private NodeViewModel CreateNode(string folderName)
        {
            return new NodeViewModel()
            {
                Name = folderName,
                IsFolder = true,
                IconPath = IconCategory.Folder.Value,
                Parent = _treeView.SelectedNodes[0]
            };
        }
    }
}
