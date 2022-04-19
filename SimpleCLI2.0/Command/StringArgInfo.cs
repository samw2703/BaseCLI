using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCLI.Extensions;

namespace SimpleCLI.Command
{
	public class StringArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
        internal StringArgInfo(string flag, string friendlyName, string propertyName, bool mandatory = false) 
			: base(flag, friendlyName, propertyName, mandatory)
		{
		}

		protected override string TypeDescription => "A string";

        internal override void Validate(List<string> args)
		{
			ValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ValidationHelper.ValidateSingleArgument(args, this);
			ValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ValidationHelper.RemoveKeysAndValuesFromArgs(args,this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag)
				.SingleOrDefault();
			if (value == null)
				return;

			ParserHelper.SetPropertyValue(parsedArgs, Flag, value);
		}
	}
}