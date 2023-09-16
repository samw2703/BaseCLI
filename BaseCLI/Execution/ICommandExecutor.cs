using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCLI.Conductor;
using BaseCLI.ReflectionObjects;

namespace BaseCLI.Execution
{
	internal interface ICommandExecutor
	{
        Task Execute(CommandReflectionObject command, List<string> args);
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

        public async Task Execute(CommandReflectionObject command, List<string> args)
        {
            var parsedArgs = Parse(command, args);
            await ExecuteCommand(command, parsedArgs);
        }

        private async Task ExecuteCommand(CommandReflectionObject command, object parsedArgs)
        {
            var task = (Task)command.GetType()
                .GetMethod(nameof(CommandReflectionObject.Execute))
                .MakeGenericMethod(_commandCatalogue.GetParsedArgTypeForCommand(command.CommandType))
                .Invoke(command, new[] { parsedArgs });
            await task.ConfigureAwait(false);
        }

        private object Parse(CommandReflectionObject command, List<string> args)
            => _flagParser
                .GetType()
                .GetMethod(nameof(IFlagParser.Parse))
                .MakeGenericMethod(_commandCatalogue.GetParsedArgTypeForCommand(command.CommandType))
                .Invoke(_flagParser, new object[] { args, command.ArgInfos });
    }
}