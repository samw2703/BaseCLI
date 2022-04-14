using System.Collections.Generic;

namespace SimpleCLI.Command
{
	public interface ICommand
	{
		string Name { get; }
		string Description { get; }
		List<ArgInfo> ArgInfos { get; }
	}

	public interface ICommand<TParsedArgs> : ICommand where TParsedArgs : ParsedArgs
	{
		void Execute(TParsedArgs args);
	}
}