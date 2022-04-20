using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SimpleCLI.Help;
using SimpleCLI.ReflectionObjects;
using SimpleCLI.Tests.TestCommands;

namespace SimpleCLI.Tests.Help
{
	public class HelperTests
	{
		private Helper _helper;

		[SetUp]
		public void Setup()
		{
			_helper = TestCommandWireUp
				.WireUpTestCommandsAndCreateServiceProvider()
				.GetRequiredService<Helper>();
		}
		
		[Test]
		public void GetToolHelp_GetsListOfCommandAndDescriptions()
		{
			const string expectedHelpText = @"f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
6ea27e30-2d8e-43ea-a9d9-bb212fe199a0 - I am an extremely complicated command";

			Assert.AreEqual(expectedHelpText, _helper.GetToolHelp());
		}

		[Test]
		public void GetCommandHelp_GetExpectedHelpText()
		{
			const string expectedHelpText = @"f3a76134-967a-4328-9765-8f7d388b9596 - I am the first test command
-str Strrring - A string
-intt intager - An integer";

			Assert.AreEqual(expectedHelpText, _helper.GetCommandHelp(new CommandReflectionObject(new TestCommand1())));
		}

		[Test]
		public void GetCommandHelp_ForCommandWithMandatoryFlag_GetExpectedHelpText()
		{
			const string expectedHelpText = @"6ea27e30-2d8e-43ea-a9d9-bb212fe199a0 - I am an extremely complicated command
-m An arg (mandatory) - A string";

			Assert.AreEqual(expectedHelpText, _helper.GetCommandHelp(new CommandReflectionObject(new TestCommand2())));
		}
	}
}
