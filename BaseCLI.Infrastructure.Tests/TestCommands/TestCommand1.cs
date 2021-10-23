using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Tests.TestCommands
{
	public class TestCommand1 : ICommand<TestCommand1ParsedArgs>
	{
		public string Name => "f3a76134-967a-4328-9765-8f7d388b9596";
		public string Description => "I am the first test command";

		public List<global::SimpleCLI.Command.ArgInfo> ArgInfos => new()
		{
			new StringArgInfo("str", "Strrring"),
			new IntArgInfo("intt", "intager")
		};
		public void Execute(TestCommand1ParsedArgs args)
		{
			var message = $@"string arg: {args.StringArg}
int arg: {args.IntArg}";
			TestConsole.WriteLine(message);
		}
	}

	public class TestCommand1ParsedArgs : ParsedArgs
	{
		[Flag("str")]
		public string StringArg { get; set; }
		[Flag("intt")]
		public int IntArg { get; set; }
	}

	public static class TestConsole
	{
		public static List<string> Messages { get; } = new();

		public static void WriteLine(string message)
		{
			Messages.Add(message);
		}
	}
}
