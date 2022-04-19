using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCLI.Extensions;

namespace SimpleCLI.Command
{
	public class IntCollectionArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
		public IntCollectionArgInfo(string flag, string friendlyName, bool mandatory = false) 
            : base(flag, friendlyName, mandatory)
		{
		}

		protected override string TypeDescription => "A collection of integers";

        internal override void Validate(List<string> args)
		{
			ValidationHelper.ValidateMandatoryArgIsPresent(args, this);
			ValidationHelper.ValidateEnoughArgsForThereToBeValue(args, this);
			ValidationHelper.ValidateValuesAreIntegers(args, this);
			ValidationHelper.RemoveKeysAndValuesFromArgs(args, this);
		}

		internal override void Parse(TArgs parsedArgs, List<string> args)
		{
			var value = args
				.GetValuesForFlag(Flag)
				.Select(x => Convert.ToInt32((string?) x))
				.ToList();

			ParserHelper.SetPropertyValue(parsedArgs, Flag, value);
		}
	}
}