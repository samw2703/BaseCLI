using System.Collections.Generic;
using System.Linq;

namespace BaseCLI
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
			ArgInfoValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ArgInfoValidationHelper.ValidateSingleArgument(args, this);
			ArgInfoValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ArgInfoValidationHelper.RemoveKeysAndValuesFromArgs(args,this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag)
				.SingleOrDefault();
			if (value == null)
				return;

			ArgInfoParserHelper.SetPropertyValue(parsedArgs, PropertyName, value);
		}
	}
}