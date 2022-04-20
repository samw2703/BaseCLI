using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCLI.Conductor
{
	internal interface ICommandCatalogue
	{
		Type GetParsedArgTypeForCommand(Type commandType);
		Type GetCommandType<TArgs>();
		List<Type> GetCommandTypes();
	}

    internal class CommandCatalogue : ICommandCatalogue
    {
        private readonly Dictionary<Type, Type> _argToCommand;

        public CommandCatalogue(Dictionary<Type, Type> argToCommand)
        {
            _argToCommand = argToCommand;
        }

        public Type GetParsedArgTypeForCommand(Type commandType)
        {
            return _argToCommand.ContainsValue(commandType)
                ? _argToCommand.Single(x => x.Value == commandType).Key
                : throw new ParsedArgsWireupException(commandType);
        }

        public List<Type> GetCommandTypes()
            => _argToCommand
                .Values
                .ToList();

        public Type GetCommandType<TArgs>() => _argToCommand[typeof(TArgs)];

    }
}