using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.FlagParsing
{
	internal interface IFlagParser
	{
		void Parse<TParsedArgs>(TParsedArgs parsedArgs, string flag, List<string> args) where TParsedArgs : ParsedArgs;
	}
}