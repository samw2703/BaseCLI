using System;
using System.Collections.Generic;
using SimpleCLI.Extensions;

namespace SimpleCLI.Command
{
	public class StringCollectionArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
        internal StringCollectionArgInfo(string flag, string friendlyName, string propertyName, bool mandatory = false) 
			: base(flag, friendlyName, propertyName, mandatory)
		{
		}

		protected override string TypeDescription => "A collection of strings";

        internal override void Validate(List<string> args)
		{
			ValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ValidationHelper.RemoveKeysAndValuesFromArgs(args, this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag);
			if (value == null)
				return;

			ParserHelper.SetPropertyValue(parsedArgs, Flag, value);
		}
	}
}