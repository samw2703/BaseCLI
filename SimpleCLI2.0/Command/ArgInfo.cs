using System;
using System.Collections.Generic;

namespace SimpleCLI.Command
{
	public abstract class ArgInfo<TArgs> where TArgs : new()
	{
		internal string Flag { get; }
		internal string FriendlyName { get; }
		internal bool Mandatory { get; }
		internal Action<TArgs, object> SetArg { get; }
		protected abstract string TypeDescription { get; }
		internal abstract Type MatchingParsedArgsPropertyType { get; }

		public ArgInfo(string flag, string friendlyName, Action<TArgs, object> setArg, bool mandatory = false)
		{
			Flag = flag;
			FriendlyName = friendlyName;
            SetArg = setArg;
            Mandatory = mandatory;
		}

		internal string GetHelp()
			=> $"-{Flag} {FriendlyName}{GetMandatoryText(Mandatory)} - {TypeDescription}";

		internal abstract void Validate(List<string> args);

		internal abstract void Parse(TArgs parsedArgs, List<string> args);

		private string GetMandatoryText(bool mandatory)
			=> mandatory ? " (mandatory)" : "";
	}
}