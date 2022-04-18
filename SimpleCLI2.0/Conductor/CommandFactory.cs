using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCLI.Command;

namespace SimpleCLI.Conductor
{
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
            new(GetCommands().SingleOrDefault(cmd => cmd.Name == commandName));

		public ICommand<TArgs> GetCommand<TArgs>() where TArgs : new()
			=> (ICommand<TArgs>)_serviceProvider.GetService(_commandCatalogue.GetCommandType<TArgs>());

		public List<CommandReflectionObject> GetCommands()
            => _commandCatalogue.GetCommandTypes()
				.Select(commandType => new CommandReflectionObject(_serviceProvider.GetService(commandType)))
				.ToList();
		
    }
}