using System.Linq;
using BaseCLI.Conductor;
using BaseCLI.Tests.TestCommands;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BaseCLI.Tests.Conductor
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