using System;

namespace DatabaseTask.Services.Commands.Interfaces
{
    public interface ICompositeCommandBuilder
    {
        public ICompositeCommandBuilder Add<T>(params object[] parameters) where T : ICommand;
        public ICommand Build();
    }
}
