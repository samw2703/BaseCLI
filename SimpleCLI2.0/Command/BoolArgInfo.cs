using System;
using System.Collections.Generic;

namespace SimpleCLI.Command
{
	public class BoolArgInfo : ArgInfo
	{
		public BoolArgInfo(string flag, string friendlyName) 
			: base(flag, friendlyName, false)
		{
		}

		protected override string TypeDescription => "A boolean";
		internal override Type MatchingParsedArgsPropertyType => typeof(bool);

		internal override void Validate(List<string> args)
		{
			ValidationHelper.ValidateSingleArgument(args, this);
			ValidationHelper.RemoveKeysFromArgs(args, this);
		}

		internal override void Parse<TParsedArgs>(TParsedArgs parsedArgs, List<string> args)
		{
			var flagPresent = args.Contains($"-{Flag}");
			if (flagPresent)
				ParserHelper.SetPropertyValue(parsedArgs, Flag, true);
		}
	}
}