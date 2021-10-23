using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using SimpleCLI.Command;
using SimpleCLI.Conductor;
using SimpleCLI.Execution;
using SimpleCLI.Help;
using SimpleCLI.Tests.TestCommands;
using SimpleCLI.Validation;

namespace SimpleCLI.Tests.Conductor
{
	public class CommandConductorTests
	{
		private TestConsole _console;
		private Mock<ICommandExecutor> _defaultMockCommandExecutor;

		[SetUp]
		public void Setup()
		{
			_console = new TestConsole();
			_defaultMockCommandExecutor = new Mock<ICommandExecutor>();
		}

		[Test]
		public void Conduce_NoCommandAndHelpFlagPassedIn_PrintsOutToolHelpAndDoesNotExecuteCommand()
		{
			var args = new[] {"-h"};
			Conduce(args);

			const string expectedMessage = @"f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
6ea27e30-2d8e-43ea-a9d9-bb212fe199a0 - I am an extremely complicated command";
			Assert.AreEqual(expectedMessage, _console.Messages.Single());
			_defaultMockCommandExecutor.Verify(x => x.Execute(It.IsAny<ICommand>(), It.IsAny<List<string>>()), Times.Never);
		}

		[Test]
		public void Conduce_WithInvalidCommandText_PrintsOutToolHelpAndDoesNotExecuteCommand()
		{
			var args = new[] { "invalid command text", "summat else" };
			Conduce(args);

			const string expectedMessage = @"f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
6ea27e30-2d8e-43ea-a9d9-bb212fe199a0 - I am an extremely complicated command";
			Assert.AreEqual(expectedMessage, _console.Messages.Single());
			_defaultMockCommandExecutor.Verify(x => x.Execute(It.IsAny<ICommand>(), It.IsAny<List<string>>()), Times.Never);
		}

		[Test]
		public void Conduce_WithCommandAndHelpFlagPassedIn_PrintsCommandHelpAndDoesNotExecuteCommand()
		{
			var args = new[] { "f3a76134-967a-4328-9765-8f7d388b9596", "-h" };
			Conduce(args);

			const string expectedMessage = @"f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
-str Strrring - A string
-intt intager - An integer";
			Assert.AreEqual(expectedMessage, _console.Messages.Single());
			_defaultMockCommandExecutor.Verify(x => x.Execute(It.IsAny<ICommand>(), It.IsAny<List<string>>()), Times.Never);
		}

		[Test]
		public void Conduce_ValidatorThrowsArgValidatorException_PrintsErrorMessageAndCommandHelpAndDoesNotExecuteCommand()
		{
			const string errorMessage = "Something went really wrong";
			var args = new[] { "f3a76134-967a-4328-9765-8f7d388b9596" };
			var mockValidator = new Mock<IValidator>();
			mockValidator
				.Setup(x => x.Validate(It.IsAny<List<string>>(), It.IsAny<List<global::SimpleCLI.Command.ArgInfo>>()))
				.Throws(new ArgValidatorException(errorMessage));
			Conduce(args, mockValidator.Object);

			var expectedMessage = $@"{errorMessage}

f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
-str Strrring - A string
-intt intager - An integer";
			Assert.AreEqual(expectedMessage, _console.Messages.Single());
			_defaultMockCommandExecutor.Verify(x => x.Execute(It.IsAny<ICommand>(), It.IsAny<List<string>>()), Times.Never);
		}

		[Test]
		public void Conduce_ValidatorThrowsOtherException_PrintsGenericErrorMessageAndCommandHelpAndDoesNotExecuteCommand()
		{
			var args = new[] { "f3a76134-967a-4328-9765-8f7d388b9596" };
			var mockValidator = new Mock<IValidator>();
			mockValidator
				.Setup(x => x.Validate(It.IsAny<List<string>>(), It.IsAny<List<global::SimpleCLI.Command.ArgInfo>>()))
				.Throws(new Exception());
			Conduce(args, mockValidator.Object);

			const string expectedMessage = @"Something went wrong, please check the arguments you passed in are correct

f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
-str Strrring - A string
-intt intager - An integer";
			Assert.AreEqual(expectedMessage, _console.Messages.Single());
			_defaultMockCommandExecutor.Verify(x => x.Execute(It.IsAny<ICommand>(), It.IsAny<List<string>>()), Times.Never);
		}

		[Test]
		public void Conduce_ArgsValid_ExecutesCommand()
		{
			var args = new[] { "f3a76134-967a-4328-9765-8f7d388b9596" };
			var mockExecutor = new Mock<ICommandExecutor>();
			Conduce(args, commandExecutor: mockExecutor.Object);

			mockExecutor.Verify(x => x.Execute(It.IsAny<TestCommand1>(), It.IsAny<List<string>>()), Times.Once);
		}

		[Test]
		public void Conduce_CommandExecutorThrowsException_PrintsGenericMessageAndCommandHelp()
		{
			var args = new[] { "f3a76134-967a-4328-9765-8f7d388b9596" };
			var mockExecutor = new Mock<ICommandExecutor>();
			mockExecutor
				.Setup(x => x.Execute(It.IsAny<ICommand>(), It.IsAny<List<string>>()))
				.Throws(new Exception());
			Conduce(args, commandExecutor: mockExecutor.Object);

			const string expectedMessage = @"Something went wrong, please check the arguments you passed in are correct

f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
-str Strrring - A string
-intt intager - An integer";
			Assert.AreEqual(expectedMessage, _console.Messages.Single());
		}

		private void Conduce(string[] args, IValidator validator = null, ICommandExecutor commandExecutor = null)
		{
			var sp = TestCommandWireUp.WireUpTestCommandsAndCreateServiceProvider();
			new CommandConductor(sp.GetRequiredService<ICommandFactory>(),
					validator ?? sp.GetRequiredService<IValidator>(),
					sp.GetRequiredService<CommandAndArgsParser>(),
					sp.GetRequiredService<Helper>(),
					_console,
					commandExecutor ?? _defaultMockCommandExecutor.Object)
				.Conduce(args);
		}

		private class TestConsole : IConsole
		{
			public List<string> Messages { get; } = new();
			public void WriteLine(string message)
			{
				Messages.Add(message);
			}
		}
	}
}