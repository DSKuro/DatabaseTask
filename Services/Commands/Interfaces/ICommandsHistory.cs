using DatabaseTask.Services.Commands.Base.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICommandsHistory
    {
        public void AddItemCommand(ICommand command);
        public void AddCommand(IResultCommand command);
        public void RemoveCommand();
        public void AddDatabaseCommand(IResultCommand command);
        public void RemoveDatabaseCommand();
        public void ExecuteUndoItemsCommands();
        public List<bool> ExecuteAllCommands();
        public void ClearAll();
    }
}
