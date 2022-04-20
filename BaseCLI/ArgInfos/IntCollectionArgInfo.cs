using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseCLI
{
    internal class IntCollectionArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
        internal IntCollectionArgInfo(string flag, string friendlyName, string propertyName, bool mandatory = false) 
            : base(flag, friendlyName, propertyName, mandatory)
		{
		}

		protected override string TypeDescription => "A collection of integers";

        internal override void Validate(List<string> args)
		{
			ArgInfoValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ArgInfoValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ArgInfoValidationHelper.ValidateValuesAreIntegers(args, this);
			ArgInfoValidationHelper.RemoveKeysAndValuesFromArgs(args, this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag)
				.Select(x => Convert.ToInt32((string?) x))
				.ToList();

			ArgInfoParserHelper.SetPropertyValue(parsedArgs, PropertyName, value);
		}
	}
}