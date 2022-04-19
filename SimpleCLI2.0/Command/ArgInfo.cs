using System;
using System.Collections.Generic;

namespace SimpleCLI.Command
{
	public abstract class ArgInfo<TArgs> where TArgs : new()
	{
		internal string Flag { get; }
		internal string FriendlyName { get; }
		internal string PropertyName { get; }
        internal bool Mandatory { get; }
        protected abstract string TypeDescription { get; }

        public ArgInfo(string flag, string friendlyName, string propertyName, bool mandatory = false)
		{
			Flag = flag;
			FriendlyName = friendlyName;
			PropertyName = propertyName;
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