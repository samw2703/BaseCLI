﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseCLI.Tests.TestCommands
{
	public class TestCommand1 : ICommand<TestCommand1ParsedArgs>
	{
		public string Name => "f3a76134-967a-4328-9765-8f7d388b9596";
		public string Description => "I am the first test command";

        public ArgInfoCollection<TestCommand1ParsedArgs> ArgInfoCollection { get; } = new(
                new()
                {
                    new StringArgInfo<TestCommand1ParsedArgs>("str", "Strrring", "StringArg"),
                    new IntArgInfo<TestCommand1ParsedArgs>("intt", "intager", "IntArg")
                });

		public Task Execute(TestCommand1ParsedArgs args)
		{
			var message = $@"string arg: {args.StringArg}
int arg: {args.IntArg}";
			TestConsole.WriteLine(message);
			return Task.CompletedTask;
		}
	}

	public class TestCommand1ParsedArgs
	{
        public string StringArg { get; set; }
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
