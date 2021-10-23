using System;
using System.Collections.Generic;

namespace SimpleCLI.Conductor
{
	internal interface ICommandCatalogue
	{
		Type GetParsedArgTypeForCommand(Type commandType);
		List<Type> GetCommandTypes();
	}
}