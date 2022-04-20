using System.Collections.Generic;

namespace BaseCLI
{
	internal class BoolArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
		internal BoolArgInfo(string flag, string friendlyName, string propertyName) 
			: base(flag, friendlyName, propertyName, false)
		{
		}

		protected override string TypeDescription => "A boolean";

        internal override void Validate(List<string> args)
		{
			ArgInfoValidationHelper.ValidateSingleArgument(args, this);
			ArgInfoValidationHelper.RemoveKeysFromArgs(args, this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var flagPresent = args.Contains($"-{Flag}");
			if (flagPresent)
				ArgInfoParserHelper.SetPropertyValue(parsedArgs, PropertyName, true);
		}
	}
}