using System;
using System.Linq;
using System.Reflection;
using SimpleCLI.Command;
using SimpleCLI.Conductor;

namespace SimpleCLI.Help
{
    internal class Helper
	{
		private readonly ICommandFactory _commandFactory;

		public Helper(ICommandFactory commandFactory)
		{
			_commandFactory = commandFactory;
		}

		public string GetCommandHelp(CommandReflectionObject command)
		{
			var helpStrings = command.ArgInfos
				.Select(info => info.GetHelp());

			return @$"{command.Name} - {command.Description}
{string.Join(Environment.NewLine, helpStrings)}";
		}

		public string GetToolHelp()
		{
			var commands = _commandFactory.GetCommands();

			return string.Join(Environment.NewLine, commands.Select(x => $"{x.Name} - {x.Description}"));
		}
	}
}
