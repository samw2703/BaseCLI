using System;
using System.Collections.Generic;

namespace SimpleCLI.Command
{
	public abstract class ArgInfo
	{
		internal string Flag { get; }
		internal string FriendlyName { get; }
		internal bool Mandatory { get; }
		protected abstract string TypeDescription { get; }
		internal abstract Type MatchingParsedArgsPropertyType { get; }

		public ArgInfo(string flag, string friendlyName, bool mandatory = false)
		{
			Flag = flag;
			FriendlyName = friendlyName;
			Mandatory = mandatory;
		}

		internal string GetHelp()
			=> $"-{Flag} {FriendlyName}{GetMandatoryText(Mandatory)} - {TypeDescription}";

		internal abstract void Validate(List<string> args);

		internal abstract void Parse<TParsedArgs>(TParsedArgs parsedArgs, List<string> args) where TParsedArgs : ParsedArgs;

		private string GetMandatoryText(bool mandatory)
			=> mandatory ? " (mandatory)" : "";
	}
}