using System.Collections.Generic;
using SimpleCLI.Command;
using SimpleCLI.Conductor;

namespace SimpleCLI.Execution
{
	internal interface ICommandExecutor
	{
		void Execute(CommandReflectionObject command, List<string> args);
	}

    internal class CommandExecutor : ICommandExecutor
    {
        private readonly IFlagParser _flagParser;
        private readonly ICommandCatalogue _commandCatalogue;

        public CommandExecutor(IFlagParser flagParser, ICommandCatalogue commandCatalogue)
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
                .GetMethod(nameof(CommandReflectionObject.Execute))
                .MakeGenericMethod(_commandCatalogue.GetParsedArgTypeForCommand(command.CommandType))
                .Invoke(command, new[] { parsedArgs });

        private object Parse(CommandReflectionObject command, List<string> args)
            => _flagParser
                .GetType()
                .GetMethod(nameof(IFlagParser.Parse))
                .MakeGenericMethod(_commandCatalogue.GetParsedArgTypeForCommand(command.CommandType))
                .Invoke(_flagParser, new object[] { args, command.ArgInfos });
    }
}