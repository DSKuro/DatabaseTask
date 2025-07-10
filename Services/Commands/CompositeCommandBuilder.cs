using DatabaseTask.Services.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseTask.Services.Commands
{
    public class CompositeCommandBuilder : ICompositeCommandBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CompositeCommand _command = new CompositeCommand();

        public CompositeCommandBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
