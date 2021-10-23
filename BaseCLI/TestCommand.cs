using System.Collections.Generic;
using SimpleCLI.Command;

namespace BaseCLI
{
	public class TestCommand : ICommand<TestParsedArgs>
	{
		private readonly IHelloSayer _helloSayer;

		public TestCommand(IHelloSayer helloSayer)
		{
			_helloSayer = helloSayer;
		}

		public void Execute(TestParsedArgs args)
		{
			var message = $@"string arg: {args.StringArg}
int arg: {args.IntArg}";

			_helloSayer.SayHello();
			System.Console.WriteLine(message);
		}

		public string Name => "Test";
		public string Description => "This is a test command";

		public List<ArgInfo> ArgInfos => new()
		{
			new StringArgInfo("s", "String arg"),
			new IntArgInfo("i", "Int arg")
		};
	}

	public class TestParsedArgs : ParsedArgs
	{
		[Flag("s")]
		public string StringArg { get; set; }
		[Flag("i")]
		public int IntArg { get; set; }
	}
}