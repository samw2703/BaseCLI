using System;

namespace SimpleCLI.Tests.TestCommands
{
	public class TestCommand2 : ICommand<TestCommand2ParsedArgs>
    {
        public void Execute(TestCommand2ParsedArgs args)
		{
			throw new NotImplementedException();
		}

		public string Name => "6ea27e30-2d8e-43ea-a9d9-bb212fe199a0";
		public string Description => "I am an extremely complicated command";
        public ArgInfoCollection<TestCommand2ParsedArgs> ArgInfoCollection { get; } = new(
            new()
            {
                new StringArgInfo<TestCommand2ParsedArgs>("m", "An arg", "", true)
            });
	}

	public class TestCommand2ParsedArgs
	{
	}
}