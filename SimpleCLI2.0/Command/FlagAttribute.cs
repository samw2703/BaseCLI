using System;

namespace SimpleCLI.Command
{
	public class FlagAttribute : Attribute
	{
		public string Flag { get; }

		public FlagAttribute(string flag)
		{
			Flag = flag;
		}
	}
}