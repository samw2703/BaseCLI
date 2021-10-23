using System;
using System.Collections.Generic;
using SimpleCLI.Extensions;

namespace SimpleCLI.Command
{
	public class StringCollectionArgInfo : ArgInfo
	{
		public StringCollectionArgInfo(string flag, string friendlyName, bool mandatory = false) 
			: base(flag, friendlyName, mandatory)
		{
		}

		protected override string TypeDescription => "A collection of strings";
		internal override Type MatchingParsedArgsPropertyType => typeof(List<string>);

		internal override void Validate(List<string> args)
		{
			ValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ValidationHelper.RemoveKeysAndValuesFromArgs(args, this);
		}

		internal override void Parse<TParsedArgs>(TParsedArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag);
			if (value == null)
				return;

			ParserHelper.SetPropertyValue(parsedArgs, Flag, value);
		}
	}
}