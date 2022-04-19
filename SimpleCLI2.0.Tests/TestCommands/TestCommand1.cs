using System.Collections.Generic;
using SimpleCLI.Command;

namespace SimpleCLI.Tests.TestCommands
{
	public class TestCommand1 : ICommand<TestCommand1ParsedArgs>
	{
		public string Name => "f3a76134-967a-4328-9765-8f7d388b9596";
		public string Description => "I am the first test command";

		public List<ArgInfo<TestCommand1ParsedArgs>> ArgInfos => new()
		{
			new StringArgInfo<TestCommand1ParsedArgs>("str", "Strrring"),
			new IntArgInfo<TestCommand1ParsedArgs>("intt", "intager")
		};
		public void Execute(TestCommand1ParsedArgs args)
		{
			var message = $@"string arg: {args.StringArg}
int arg: {args.IntArg}";
			TestConsole.WriteLine(message);
		}
	}

	public class TestCommand1ParsedArgs
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
