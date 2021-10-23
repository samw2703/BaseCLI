using System;

namespace SimpleCLI.Validation
{
	internal class ArgValidatorException : Exception
	{
		public ArgValidatorException(string message)
			: base(message)
		{
		}
	}
}