using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Validation
{
	internal interface IValidator
	{
		void Validate(List<string> args, List<ArgInfoReflectionObject> argInfos);
	}
}