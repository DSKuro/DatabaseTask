using DatabaseTask.Services.Commands.Base.Interfaces;
using DatabaseTask.Services.Commands.DatabaseCommands.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICommandsHistory
    {
        public void AddItemCommand(ICommand command);
        public void AddCommand(IResultCommand command);
        public void RemoveCommand();
        public void AddDatabaseCommand(IDatabaseCommand command);
        public void RemoveDatabaseCommand();
        public void ExecuteUndoItemsCommands();
        public List<bool> ExecuteAllCommands();
        public void ClearAll();
        public int GetCommandsCount();
    }
}
