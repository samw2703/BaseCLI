using System.Collections.Generic;
using SimpleCLI.Command;
using SimpleCLI.Conductor;
using SimpleCLI.FlagParsing;

namespace SimpleCLI.Execution
{
	internal class CommandExecutor : ICommandExecutor
	{
		private readonly FlagParser _flagParser;
		private readonly ICommandCatalogue _commandCatalogue;

		public CommandExecutor(FlagParser flagParser, ICommandCatalogue commandCatalogue)
		{
			_flagParser = flagParser;
			_commandCatalogue = commandCatalogue;
		}

		public void Execute(CommandReflectionObject command, List<string> args)
		{
			var parsedArgs = Parse(command, args);
			ExecuteCommand(command, parsedArgs);
		}

		private void ExecuteCommand(CommandReflectionObject command, object parsedArgs)
			=> command.GetType()
				.GetMethod(nameof(ICommand<object>.Execute))
				.Invoke(command, new[] {parsedArgs});

		private object Parse(CommandReflectionObject command, List<string> args)
			=> _flagParser
				.GetType()
				.GetMethod(nameof(FlagParser.Parse))
				.MakeGenericMethod(_commandCatalogue.GetParsedArgTypeForCommand(command.GetType()))
				.Invoke(_flagParser, new object[] {args, command.ArgInfos});
	}
}