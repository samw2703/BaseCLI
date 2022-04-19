using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SimpleCLI.Execution;
using SimpleCLI.ReflectionObjects;
using SimpleCLI.Tests.TestCommands;

namespace SimpleCLI.Tests.Execution
{
	public class CommandExecutorTests
	{
		[Test]
		public void Execute_DoesExecuteTheCommand()
		{
			const string stringArg = "I am string arg";
			const int intArg = 497;
			var commandExecutor = TestCommandWireUp
				.WireUpTestCommandsAndCreateServiceProvider()
				.GetRequiredService<ICommandExecutor>();
			commandExecutor.Execute(new CommandReflectionObject(new TestCommand1()), new List<string>
			{
				"-str",
				stringArg,
				"-intt",
				intArg.ToString()
			});

			var expectedMessage = $@"string arg: {stringArg}
int arg: {intArg}";
			Assert.True(TestConsole.Messages.Any(x => x == expectedMessage));
		}
	}
}