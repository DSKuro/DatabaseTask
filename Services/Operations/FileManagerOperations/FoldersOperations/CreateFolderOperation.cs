using DatabaseTask.Models.Categories;
using DatabaseTask.Services.DataGrid.DataGridFunctionality.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CreateFolderOperation : ICreateFolderOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;
        private readonly IDataGridFunctionality _dataGridFunctionality;

        public CreateFolderOperation(ITreeViewFunctionality treeViewFunctionality,
            IDataGridFunctionality dataGridFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
            _dataGridFunctionality = dataGridFunctionality;
        }

        public async Task CreateFolder(INode parent, string folderName)
        {
            if (parent is not null)
            {
                await CreateFolderImplementation(folderName, parent);
            }
        }

        private async Task CreateFolderImplementation(string folderName, INode parent)
        {
            NodeViewModel node = CreateNode(folderName, parent);
            int index = 0;
            bool isInsert = _treeViewFunctionality.TryInsertNode(parent, node, out index);
            if (isInsert)
            {
                await UpdateProperties(index, parent, node) ;
            }
        }

        private async Task UpdateProperties(int index, INode parent, INode node)
        {
            _dataGridFunctionality.AddProperties(CreateFileProperties(node));
            _treeViewFunctionality.UpdateSelectedNodes(node);
            await Task.Delay(100);
            _treeViewFunctionality.BringIntoView(node);
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

        private NodeViewModel CreateNode(string folderName, INode parent)
        {
            return new NodeViewModel()
            {
                Name = folderName,
                IsFolder = true,
                IconPath = IconCategory.Folder.Value,
                Parent = parent
            };
        }

        public void UndoCreateFolder(INode parent, string folderName)
        {
            if (parent is not null)
            {
                var node = parent.Children
                    .FirstOrDefault(item => item.Name.Equals(folderName));
                if (node is not null)
                {
                    _treeViewFunctionality.RemoveNode(node);
                    _dataGridFunctionality.RemoveProperties(node);
                }
            }
        }
    }
}
