using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCLI.Execution;
using BaseCLI.ReflectionObjects;
using BaseCLI.Tests.TestCommands;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BaseCLI.Tests.Execution
{
	public class CommandExecutorTests
	{
		[Test]
		public async Task Execute_DoesExecuteTheCommand()
		{
			const string stringArg = "I am string arg";
			const int intArg = 497;
			var commandExecutor = TestCommandWireUp
				.WireUpTestCommandsAndCreateServiceProvider()
				.GetRequiredService<ICommandExecutor>();
			await commandExecutor.Execute(new CommandReflectionObject(new TestCommand1()), new List<string>
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