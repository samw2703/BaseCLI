using System;
using System.Collections.Generic;
using BaseCLI.Conductor;
using BaseCLI.Execution;
using BaseCLI.Help;
using BaseCLI.Tests.TestCommands;
using BaseCLI.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCLI.Tests
{
	public static class TestCommandWireUp
	{
		public static IServiceProvider WireUpTestCommandsAndCreateServiceProvider()
		{
			var sc = new ServiceCollection();
			sc.AddSingleton<TestCommand1>();
			sc.AddSingleton<TestCommand2>();
			sc.AddSingleton<ICommandCatalogue>(CreateCommandCatalogue());
			sc.AddSingleton<ICommandExecutor, CommandExecutor>();
			sc.AddSingleton<IFlagParser, FlagParser>();
			sc.AddSingleton<ICommandFactory, CommandFactory>();
			sc.AddSingleton<CommandAndArgsParser>();
			sc.AddSingleton<Helper>();
			sc.AddSingleton<IValidator, Validator>();
			sc.AddSingleton<ICommandFactory, CommandFactory>();

			return sc.BuildServiceProvider();
		}

		private static CommandCatalogue CreateCommandCatalogue()
		{
			var dictionary = new Dictionary<Type, Type>
			{
				{typeof(TestCommand1ParsedArgs), typeof(TestCommand1)},
				{typeof(TestCommand2ParsedArgs), typeof(TestCommand2)}
			};
			return new CommandCatalogue(dictionary);
		}
	}
}