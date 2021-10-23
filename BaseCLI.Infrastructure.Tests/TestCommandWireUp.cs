using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SimpleCLI.Conductor;
using SimpleCLI.Execution;
using SimpleCLI.FlagParsing;
using SimpleCLI.Help;
using SimpleCLI.Tests.TestCommands;
using SimpleCLI.Validation;

namespace SimpleCLI.Tests
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
			sc.AddSingleton<FlagParser>();
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
				{typeof(TestCommand1), typeof(TestCommand1ParsedArgs)},
				{typeof(TestCommand2), typeof(TestCommand2ParsedArgs)}
			};
			return new CommandCatalogue(dictionary);
		}
	}
}