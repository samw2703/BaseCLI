using System;

namespace BaseCLI
{
	public class SimpleCLISetupException : Exception
	{
		public SimpleCLISetupException(string message)
			: base(message)
		{
		}
	}
}