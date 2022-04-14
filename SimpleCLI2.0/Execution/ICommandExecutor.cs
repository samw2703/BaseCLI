using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Execution
{
	internal interface ICommandExecutor
	{
		void Execute(ICommand command, List<string> args);
	}
}