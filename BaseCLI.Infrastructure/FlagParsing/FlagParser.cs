using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.FlagParsing
{
	internal class FlagParser
	{
		public TParsedArgs Parse<TParsedArgs>(List<string> args, List<ArgInfo> argInfos) where TParsedArgs : ParsedArgs, new()
		{
			var parsedArgs = new TParsedArgs();
			foreach (var argInfo in argInfos)
				argInfo.Parse(parsedArgs, args);

			return parsedArgs;
		}
	}
}
