using DatabaseTask.Services.Commands.Base.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Database.Transaction.Interfaces
{
    public interface ICommandsTransaction
    {
        public void ExecuteCommandsInTransaction(Queue<IResultCommand> commands);
    }
}
