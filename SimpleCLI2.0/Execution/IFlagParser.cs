using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Execution
{
	internal interface IFlagParser
    {
        TArgs Parse<TArgs>(List<string> args, List<ArgInfoReflectionObject> argInfos) where TArgs : new();
    }

    internal class FlagParser : IFlagParser
    {
        public TArgs Parse<TArgs>(List<string> args, List<ArgInfoReflectionObject> argInfos) where TArgs : new()
        {
            var parsedArgs = new TArgs();
            foreach (var argInfo in argInfos)
                argInfo.Parse(parsedArgs, args);

            return parsedArgs;
        }
    }
}