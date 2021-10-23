using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Conductor
{
	internal interface ICommandFactory
	{
		ICommand GetCommand(string commandName);
		List<ICommand> GetCommands();
	}
}