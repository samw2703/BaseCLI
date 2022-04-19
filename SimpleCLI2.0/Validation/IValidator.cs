using System.Collections.Generic;
using System.Linq;
using SimpleCLI.Command;

namespace SimpleCLI.Validation
{
	internal interface IValidator
	{
		void Validate(List<string> args, List<ArgInfoReflectionObject> argInfos);
	}

    internal class Validator : IValidator
    {
        public void Validate(List<string> args, List<ArgInfoReflectionObject> argInfos)
        {
            var argsCopy = new List<string>(args);
            foreach (var argInfo in argInfos)
                argInfo.Validate(argsCopy);

            if (argsCopy.Any())
                throw new ArgValidatorException($"Unable to parse the following args:\n{string.Join("\n", argsCopy)}");
        }
    }
}