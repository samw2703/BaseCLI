using System;
using System.Collections.Generic;
using SimpleCLI.Extensions;

namespace SimpleCLI.Command
{
	public class StringCollectionArgInfo<TArgs> : ArgInfo<TArgs> where TArgs : new()
	{
		public StringCollectionArgInfo(string flag, string friendlyName, Action<TArgs, List<string>> setArg, bool mandatory = false) 
			: base(flag, friendlyName, (args, obj) => setArg(args, (List<string>)obj), mandatory)
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