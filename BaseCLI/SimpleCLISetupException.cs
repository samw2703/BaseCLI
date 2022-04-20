using System;

namespace SimpleCLI
{
	public class SimpleCLISetupException : Exception
	{
		public SimpleCLISetupException(string message)
			: base(message)
		{
		}
	}
}