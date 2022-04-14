using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SimpleCLI.Command;
using SimpleCLI.Conductor;
using SimpleCLI.Execution;
using SimpleCLI.FlagParsing;
using SimpleCLI.Help;
using SimpleCLI.Validation;
using Console = SimpleCLI.Conductor.Console;

namespace SimpleCLI
{
	public static class SimpleCLI
	{
		public static void Execute(string[] args, Assembly[] commandAssemblies, Action<ServiceCollection> setupServices = null)
		{
			var sp = Setup(commandAssemblies, setupServices);

			sp
				.GetRequiredService<CommandConductor>()
				.Conduce(args);
		}

		private static IServiceProvider Setup(Assembly[] commandAssemblies, Action<ServiceCollection> setupServices = null)
		{
			var sc = new ServiceCollection();
			sc.AddSingleton<ICommandExecutor, CommandExecutor>();
			sc.AddSingleton<FlagParser>();
			sc.AddSingleton<ICommandFactory, CommandFactory>();
			sc.AddSingleton<CommandAndArgsParser>();
			sc.AddSingleton<Helper>();
			sc.AddSingleton<IValidator, Validator>();
			sc.AddSingleton<ICommandFactory, CommandFactory>();
			sc.AddSingleton<IConsole, Console>();
			sc.AddSingleton<CommandConductor>();
			SetupCommands(sc, commandAssemblies);

			setupServices?.Invoke(sc);

			var sp = sc.BuildServiceProvider();

			ValidateCommandSetup(commandAssemblies, sp);

			return sp;
		}

		private static void SetupCommands(ServiceCollection sc, Assembly[] commandAssemblies)
		{
			var commandTypes = GetCommandTypes(commandAssemblies);
			foreach (var commandType in commandTypes)
				sc.AddSingleton(commandType);

			var catalogueDictionary = GetCommandTypeParsedArgsDictionary(commandTypes);
			sc.AddSingleton<ICommandCatalogue>(new CommandCatalogue(catalogueDictionary));
		}

		private static void ValidateCommandSetup(Assembly[] commandAssemblies, IServiceProvider sp)
		{
			var commandTypes = GetCommandTypes(commandAssemblies);
			var commandTypeArgTypeDictionary = GetCommandTypeParsedArgsDictionary(commandTypes);
			var errors = new List<string>();
			foreach (var commandTypeArgTypePair in commandTypeArgTypeDictionary)
				errors.AddRange(ValidateCommandTypeArgTypeAreSetupCorrectly(commandTypeArgTypePair.Key, commandTypeArgTypePair.Value, sp));

			if (errors.Any())
				throw new SimpleCLISetupException(string.Join(Environment.NewLine, errors));
		}

		private static List<string> ValidateCommandTypeArgTypeAreSetupCorrectly(Type commandType, Type parsedArgsType, IServiceProvider sp)
		{
			var command = sp.GetRequiredService(commandType) as ICommand;
			var argInfos = command.ArgInfos;
			var parsedArgsFlaggedProperties = GetParsedArgsFlaggedProperties(parsedArgsType);
			var errors = new List<string>();
			foreach (var argInfo in argInfos)
			{
				var matchingParsedArg = parsedArgsFlaggedProperties
					.SingleOrDefault(x => x.Flag == argInfo.Flag);
				if (matchingParsedArg == default)
				{
					errors.Add($"{command.Name}: No property found in parsed args for {argInfo.FriendlyName} flag");
					continue;
				}

				if (matchingParsedArg.Type != argInfo.MatchingParsedArgsPropertyType)
					errors.Add($"{command.Name}: property found in parsed args for {argInfo.FriendlyName} flag is of the wrong type, it should be {argInfo.MatchingParsedArgsPropertyType.Name}");
			}

			foreach (var parsedArgsFlaggedProperty in parsedArgsFlaggedProperties)
			{
				if (argInfos.All(x => x.Flag != parsedArgsFlaggedProperty.Flag))
					errors.Add($"{command.Name}: No arg info found for property with flag -{parsedArgsFlaggedProperty.Flag}");
			}

			return errors;
		}

		private static List<(string Flag, Type Type)> GetParsedArgsFlaggedProperties(Type parsedArgsType)
		{
			var flagProperties = parsedArgsType
				.GetProperties()
				.Where(x => x.GetCustomAttributes().Any(x => x.GetType() == typeof(FlagAttribute)));
			return flagProperties
				.Select(x => (x.GetCustomAttribute<FlagAttribute>().Flag, x.PropertyType))
				.ToList();
		}

		private static Dictionary<Type, Type> GetCommandTypeParsedArgsDictionary(List<Type> commandTypes)
			=> commandTypes
				.Select(ct => new KeyValuePair<Type, Type>(ct, GetParsedArgsType(ct)))
				.ToDictionary(x => x.Key, x => x.Value);

		private static List<Type> GetCommandTypes(Assembly[] commandAssemblies)
			=> commandAssemblies
				.SelectMany(x => x.GetTypes())
				.Where(type => typeof(ICommand).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
				.ToList();

		private static Type GetParsedArgsType(Type commandType)
		{
			var parsedArgsType = commandType
				.GetInterface(typeof(ICommand<ParsedArgs>).Name)
				?.GetGenericArguments()
				.SingleOrDefault();

			if (parsedArgsType == null)
				throw new SimpleCLISetupException(
					$"Unable to get parsed args type for command {commandType.FullName} please check this is setup properly");

			return parsedArgsType;
		}
	}
}