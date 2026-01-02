using DatabaseTask.Services.Operations.FileManagerOperations.Accessibility.Interfaces;
using DatabaseTask.Services.Operations.FileManagerOperations.Exceptions;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.Operations.FileManagerOperations.Accessibility
{
    public class FileManagerCommonOperationsPermissions : IFileManagerCommonOperationsPermission
    {
        public void CanDeleteItems(List<INode> nodes)
        {
            if (!nodes.Any())
            {
                throw new FileManagerOperationsException("Элемент не выбран");
            }
        }

        public void CanMoveItems(List<INode> nodes)
        {
            if (nodes.Count == 0)
            {
                throw new FileManagerOperationsException("Каталог и файлы не выбраны");
            }
            else if (nodes.Count == 1)
            {
                throw new FileManagerOperationsException("Не выбран целевой каталог");
            }
            else
            {
                List<NodeViewModel> selectedNodes = nodes.OfType<NodeViewModel>().ToList();

                if (!selectedNodes.Last().IsFolder)
                {
                    throw new FileManagerOperationsException("Вместо целевого каталога выбран файл");
                }
            }
        }
    }
}
