using System.Collections.Generic;
using System.Linq;

namespace BaseCLI.Conductor
{
	internal class CommandAndArgsParser
	{
		public (string CommandName, List<string> Args) Parse(string[] args)
		{
			if (args.Length == 0)
				return ("", new List<string>());
			var firstFlagIndex = GetIndexOfFirstFlag(args);
			if (firstFlagIndex == 0)
				return ("", args.ToList());

			return ParseFirstArgAsCommandAndRemainderAsArgs(args);
		}

		private (string CommandName, List<string> Args) ParseFirstArgAsCommandAndRemainderAsArgs(string[] args)
			=> (args.First(), args.Skip(1).Take(args.Length - 1).ToList());

		private int GetIndexOfFirstFlag(string[] args)
			=> args.ToList().IndexOf(args.FirstOrDefault(x => x.StartsWith('-')));
	}
}