using System.Collections.Generic;

namespace BaseCLI
{
    internal class StringCollectionArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
        internal StringCollectionArgInfo(string flag, string friendlyName, string propertyName, bool mandatory = false) 
			: base(flag, friendlyName, propertyName, mandatory)
		{
		}

		protected override string TypeDescription => "A collection of strings";

        internal override void Validate(List<string> args)
		{
			ArgInfoValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ArgInfoValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ArgInfoValidationHelper.RemoveKeysAndValuesFromArgs(args, this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag);
			if (value == null)
				return;

			ArgInfoParserHelper.SetPropertyValue(parsedArgs, PropertyName, value);
		}
	}
}