using DatabaseTask.Services.Commands.Interfaces;
using DatabaseTask.Services.FileManagerOperations.FoldersOperations.Interfaces;

namespace DatabaseTask.Services.Commands.ItemCommands
{
    public class CreateFolderItemCommand : ICommand
    {
        private readonly string _folderName;
        private readonly ICreateFolderOperation _folderOperation;

        public CreateFolderItemCommand(ICreateFolderOperation folderOperation, string folderName)
        {
            _folderOperation = folderOperation;
            _folderName = folderName;
        }

        public void Execute()
        {
            if (_folderOperation != null)
            {
                _folderOperation.CreateFolder(_folderName);
            }
        }

        public void Undo()
        {

        }
    }
}
