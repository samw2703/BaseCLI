using System;
using System.Collections.Generic;

namespace SimpleCLI.Command
{
	public class BoolArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
		internal BoolArgInfo(string flag, string friendlyName, string propertyName) 
			: base(flag, friendlyName, propertyName, false)
		{
		}

		protected override string TypeDescription => "A boolean";

        internal override void Validate(List<string> args)
		{
			ValidationHelper.ValidateSingleArgument(args, this);
			ValidationHelper.RemoveKeysFromArgs(args, this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var flagPresent = args.Contains($"-{Flag}");
			if (flagPresent)
				ParserHelper.SetPropertyValue(parsedArgs, Flag, true);
		}
	}
}