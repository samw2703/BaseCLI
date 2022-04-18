using System.Collections.Generic;

namespace SimpleCLI.Command
{

	public interface ICommand<TArgs>
	{
        string Name { get; }
        string Description { get; }
        List<ArgInfo> ArgInfos { get; }

		void Execute(TArgs args);
	}
}