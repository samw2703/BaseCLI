using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCLI.Command;

namespace SimpleCLI.Conductor
{
	internal class CommandFactory : ICommandFactory
	{
		private List<ICommand> _commands;
		private readonly IServiceProvider _serviceProvider;
		private readonly ICommandCatalogue _commandCatalogue;

		public CommandFactory(IServiceProvider serviceProvider, ICommandCatalogue commandCatalogue)
		{
			_serviceProvider = serviceProvider;
			_commandCatalogue = commandCatalogue;
		}

		public ICommand GetCommand(string commandName)
			=> GetCommands().SingleOrDefault(cmd => cmd.Name == commandName);

		public List<ICommand> GetCommands()
		{
			return _commands ??= _commandCatalogue
				.GetCommandTypes()
				.Select(commandType => (ICommand) _serviceProvider.GetService(commandType))
				.ToList();
		}
	}
}