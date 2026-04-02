using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Database;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Commands
{
    public class DeleteDatabaseCommand : IDatabaseCommand
    {
        private readonly string _path;
        private readonly ITblDrawingContentsRepository _drawingRepository;

        private bool _isSuccess;

        public bool IsSuccess => _isSuccess;

        public string SourcePath => _path;

        public DeleteDatabaseCommand(string path, 
            ITblDrawingContentsRepository drawingRepository)
        {
            _path = path;
            _drawingRepository = drawingRepository;
            _isSuccess = false;
        }

        public Task Execute(DataContext context, Dictionary<string, List<TblDrawingContent>> pathIndex)
        {
            try
            {
                _drawingRepository.DeleteItemContext(context, pathIndex[_path], _path);
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

        public void Commit()
        {

        }
    }
}
