using System.Collections.Generic;

namespace SimpleCLI.Command
{

	public interface ICommand<TArgs> where TArgs : new()
	{
        string Name { get; }
        string Description { get; }
        List<ArgInfo<TArgs>> ArgInfos { get; }

		void Execute(TArgs args);
	}
}