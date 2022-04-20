using System;

namespace BaseCLI.Validation
{
	internal class ArgValidatorException : Exception
	{
		public ArgValidatorException(string message)
			: base(message)
		{
		}
	}
}