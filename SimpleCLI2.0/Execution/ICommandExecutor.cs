using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Execution
{
	internal interface ICommandExecutor
	{
		void Execute(CommandReflectionObject command, List<string> args);
	}
}