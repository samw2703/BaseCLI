using System.Collections.Generic;

namespace SimpleCLI.FlagParsing
{
	internal interface IFlagParser
    {
        void Parse<TArgs>(TArgs parsedArgs, string flag, List<string> args);
    }
}