using System;

namespace SimpleCLI.Conductor
{
	internal class ParsedArgsWireupException : Exception
	{
		public ParsedArgsWireupException( Type commandType )
			:base($"No parsed args type setup for {commandType.Name} command")
		{
		}
	}
}