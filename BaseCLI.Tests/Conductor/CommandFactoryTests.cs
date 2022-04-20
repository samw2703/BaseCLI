using System.Linq;
using BaseCLI.Conductor;
using BaseCLI.Tests.TestCommands;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BaseCLI.Tests.Conductor
{
	public class CommandFactoryTests
	{
		private ICommandFactory _commandFactory;

		[SetUp]
		public void Setup()
		{
			_commandFactory = TestCommandWireUp
				.WireUpTestCommandsAndCreateServiceProvider()
				.GetRequiredService<ICommandFactory>();
		}

		[Test]
		public void GetCommands_RetrievesAllCommands()
		{
			var commands = _commandFactory.GetCommands();

			Assert.True(commands.Any(x => x.Name == new TestCommand1().Name));
			Assert.True(commands.Any(x => x.Name == new TestCommand2().Name));
        }

		[Test]
		public void GetCommand_GetsCommandWithSpecifiedName()
		{
			const string command2Name = "6ea27e30-2d8e-43ea-a9d9-bb212fe199a0";
			var command = _commandFactory.GetCommand(command2Name);

			Assert.True(command.Name == command2Name);
		}

		[Test]
		public void GetCommand_NoCommandExistsForInputtedName_ReturnsNull()
		{
			const string command2Name = "Not a command name";
			var command = _commandFactory.GetCommand(command2Name);

			Assert.Null(command);
		}
	}
}