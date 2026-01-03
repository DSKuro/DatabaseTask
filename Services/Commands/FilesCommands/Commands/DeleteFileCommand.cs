using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.FilesCommands.Commands
{
    public class DeleteFileCommand : ICommand
    {
        private readonly string _path;
        private readonly IFilesOperations _filesOperations;

        public DeleteFileCommand(string path, IFilesOperations filesOperations)
        {
            _path = path;
            _filesOperations = filesOperations;
        }

        public Task Execute()
        {
            _filesOperations.DeleteFile(_path);
            return Task.CompletedTask;
        }

        public void Undo()
        {

        }
    }
}
