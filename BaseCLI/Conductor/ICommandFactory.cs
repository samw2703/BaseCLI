using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCLI.ReflectionObjects;

namespace SimpleCLI.Conductor
{
	internal interface ICommandFactory
	{
		CommandReflectionObject GetCommand(string commandName);
        List<CommandReflectionObject> GetCommands();
	}

    internal class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandCatalogue _commandCatalogue;

        public CommandFactory(IServiceProvider serviceProvider, ICommandCatalogue commandCatalogue)
        {
            _serviceProvider = serviceProvider;
            _commandCatalogue = commandCatalogue;
        }

        public CommandReflectionObject GetCommand(string commandName) =>
            GetCommands().SingleOrDefault(cmd => cmd.Name == commandName);

        public List<CommandReflectionObject> GetCommands()
            => _commandCatalogue.GetCommandTypes()
                .Select(commandType => new CommandReflectionObject(_serviceProvider.GetService(commandType)))
                .ToList();
    }
}