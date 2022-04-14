using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCLI.Conductor
{
	internal class CommandCatalogue : ICommandCatalogue
	{
		private readonly Dictionary<Type, Type> _commandToArgsTypeDictionary;

		public CommandCatalogue(Dictionary<Type, Type> commandToArgsTypeDictionary)
		{
			_commandToArgsTypeDictionary = commandToArgsTypeDictionary;
		}

		public Type GetParsedArgTypeForCommand(Type commandType)
		{
			return _commandToArgsTypeDictionary.ContainsKey(commandType)
				? _commandToArgsTypeDictionary[commandType]
				: throw new ParsedArgsWireupException(commandType);
		}

		public List<Type> GetCommandTypes()
			=> _commandToArgsTypeDictionary
				.Keys
				.ToList();
	}
}