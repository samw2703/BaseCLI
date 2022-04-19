using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Conductor
{
	internal interface ICommandFactory
	{
		CommandReflectionObject GetCommand(string commandName);
        List<CommandReflectionObject> GetCommands();
	}
}