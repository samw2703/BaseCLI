using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCLI.Extensions;

namespace SimpleCLI.Command
{
	public class IntArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
		public IntArgInfo(string flag, string friendlyName, bool mandatory = false) 
            : base(flag, friendlyName, mandatory)
		{
		}

		protected override string TypeDescription => "An integer";
		internal override Type MatchingParsedArgsPropertyType => typeof(int);

		internal override void Validate(List<string> args)
		{
			ValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ValidationHelper.ValidateSingleArgument(args, this);
			ValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ValidationHelper.ValidateValuesAreIntegers(args, this);
			ValidationHelper.RemoveKeysAndValuesFromArgs(args, this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag)
				.SingleOrDefault();
			if (value == null)
				return;

			ParserHelper.SetPropertyValue(parsedArgs, Flag, Convert.ToInt32(value));
		}
	}
}