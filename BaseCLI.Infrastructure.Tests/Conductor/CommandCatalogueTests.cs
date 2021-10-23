using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using SimpleCLI.Conductor;
using SimpleCLI.Tests.TestCommands;

namespace SimpleCLI.Tests.Conductor
{
	public class CommandCatalogueTests
	{
		private ICommandCatalogue _commandCatalogue;

		[SetUp]
		public void Setup()
		{
			_commandCatalogue = TestCommandWireUp
				.WireUpTestCommandsAndCreateServiceProvider()
				.GetRequiredService<ICommandCatalogue>();
		}

		[Test]
		public void GetCommandTypes_GetsCommandTypes()
		{
			var types = _commandCatalogue.GetCommandTypes();

			Assert.True(types.Any(x => x == typeof(TestCommand1)));
		}
	}
}