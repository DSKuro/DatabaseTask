using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations.Interfaces;
using DatabaseTask.Services.TreeViewLogic.Functionality.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Operations.FileManagerOperations.FoldersOperations
{
    public class CreateFolderOperation : ICreateFolderOperation
    {
        private readonly ITreeViewFunctionality _treeViewFunctionality;

        public CreateFolderOperation(ITreeViewFunctionality treeViewFunctionality)
        {
            _treeViewFunctionality = treeViewFunctionality;
        }

        public async Task CreateFolder(INode parent, string folderName)
        {
            if (parent is not null)
            {
                parent.IsExpanded = true;
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
                await UpdateProperties(index, parent, node);
                node.IsOperationHighlighted = true;
            }
        }

        private async Task UpdateProperties(int index, INode parent, INode node)
        {
            _treeViewFunctionality.UpdateSelectedNodes(node);
            await Task.Delay(100);
            _treeViewFunctionality.BringIntoView(node);
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
                    _treeViewFunctionality.UpdateSelectedNodes(parent);
                    node.IsOperationHighlighted = false;
                    if (!parent.Children.Any())
                    {
                        parent.IsExpanded = false;

                        if (parent is NodeViewModel parentNode)
                        {
                            parentNode.IconPath = IconCategory.Folder.Value;
                            parentNode.IsLoaded = false;
                        }
                    }
                }
            }
        }

        public void CommitCreateFolder(INode parent, string folderName)
        {
            if (parent is not null)
            {
                var node = parent.Children
                    .FirstOrDefault(item => item.Name.Equals(folderName));
                if (node is not null)
                {
                    node.IsOperationHighlighted = false;
                }
            }
        }
    }
}
