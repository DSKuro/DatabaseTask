using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Commands
{
    public class DeleteDatabaseCommand : IResultCommand
    {
        private readonly string _path;
        private readonly ITblDrawingContentsRepository _drawingRepository;

        private bool _isSuccess;

        public bool IsSuccess => _isSuccess;

        public DeleteDatabaseCommand(string path, 
            ITblDrawingContentsRepository drawingRepository)
        {
            _path = path;
            _drawingRepository = drawingRepository;
            _isSuccess = false;
        }

        public Task Execute()
        {
            try
            {
                _drawingRepository.DeleteItem(_path);
                _isSuccess = true;
            }
            catch
            {
                _isSuccess = false;
            }

            return Task.CompletedTask;
        }

        public void Undo()
        {

        }
    }
}
