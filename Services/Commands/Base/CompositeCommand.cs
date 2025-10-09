using DatabaseTask.Services.Commands.Base.Interfaces;
using System.Collections.Generic;

namespace DatabaseTask.Services.Commands.Base
{
    public class CompositeCommand : ICommand
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void Execute()
        {
            foreach (ICommand command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {

        }
    }
}
