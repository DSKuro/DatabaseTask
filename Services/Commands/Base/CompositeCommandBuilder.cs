using DatabaseTask.Services.Commands.Base.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Services.Commands.Base
{
    public class CompositeCommandBuilder : ICompositeCommandBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CompositeCommand _command;

        public CompositeCommandBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _command = new CompositeCommand();
        }

        public ICompositeCommandBuilder Add<T>(params object[] parameters) where T : ICommand
        {
            T command = ActivatorUtilities.CreateInstance<T>(_serviceProvider, parameters);
            _command.AddCommand(command);
            return this;
        }

        public ICommand Build()
        {
            return _command;
        }
    }
}
