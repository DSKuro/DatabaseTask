using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;

namespace DatabaseTask.Services.Commands.FilesCommands
{
    public class CreateFolderCommand : ICommand
    {
        private readonly string _path;
        private readonly IFilesOperations _filesOperations;

        public CreateFolderCommand(string path, IFilesOperations filesOperations)
        {
            _path = path;
            _filesOperations = filesOperations;           
        }   

        public void Execute()
        {
            _filesOperations.CreateFolder(_path);
        }

        public void Undo()
        {

        }
    }
}
