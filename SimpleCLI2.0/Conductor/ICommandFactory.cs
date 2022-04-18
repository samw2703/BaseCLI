using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Conductor
{
	internal interface ICommandFactory
	{
		CommandReflectionObject GetCommand(string commandName);
		ICommand<TArgs> GetCommand<TArgs>() where TArgs : new();
		List<CommandReflectionObject> GetCommands();
	}
}