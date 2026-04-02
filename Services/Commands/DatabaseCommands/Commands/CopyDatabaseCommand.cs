using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using DatabaseTask.Services.Database;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Commands.DatabaseCommands.Commands
{
    public class CopyDatabaseCommand : IDatabaseCommand
    {
        private readonly string _oldPath;
        private readonly string _newPath;
        private readonly ITblDrawingContentsRepository _drawingRepository;

        private bool _isSuccess;

        public bool IsSuccess => _isSuccess;

        public string SourcePath { get => _oldPath; }

        public CopyDatabaseCommand(string oldPath, string newPath,
            ITblDrawingContentsRepository drawingRepository)
        {
            _oldPath = oldPath;
            _newPath = newPath;
            _drawingRepository = drawingRepository;
            _isSuccess = false;
        }

        public Task Execute(DataContext context, Dictionary<string, List<TblDrawingContent>> pathIndex)
        {
            try
            {
                _drawingRepository.CopyItemsContext(context, pathIndex[_oldPath], _oldPath, _newPath);
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
