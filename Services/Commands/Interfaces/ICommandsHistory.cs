using DatabaseTask.Services.Commands.Base.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICommandsHistory
    {
        public void AddCommand(IResultCommand command);
        public void RemoveCommand();
        public List<bool> ExecuteAllCommands();
        public void ClearAll();
    }
}
