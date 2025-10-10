using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System.Threading.Tasks;

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

        public Task Execute()
        {
            _filesOperations.CreateFolder(_path);
            return Task.CompletedTask;
        }

        public void Undo()
        {

        }
    }
}
