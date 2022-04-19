using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SimpleCLI.Conductor;

namespace SimpleCLI
{
	public static class SimpleCLI
	{
		public static void Execute(string[] args, Assembly[] commandAssemblies, Action<IServiceCollection> setupServices = null)
		{
			var sp = Setup(commandAssemblies, setupServices);

			sp.GetRequiredService<CommandConductor>().Conduce(args);
		}

        private static IServiceProvider Setup(Assembly[] assemblies, Action<IServiceCollection> setupServices = null)
        {
            var sc = new ServiceCollection();
			sc.AddCliServices();
			SetupCommands(sc, assemblies);

			setupServices?.Invoke(sc);

			var sp = sc.BuildServiceProvider();

            return sp;
		}

        private static void SetupCommands(ServiceCollection sc, Assembly[] assemblies)
        {
            var commandAssemblies = new CommandSetupService(assemblies);
            commandAssemblies.AddCommands(sc);
			commandAssemblies.AddCommandCatalogue(sc);
        }

        private class CommandSetupService
        {
            private readonly Dictionary<Type, Type> _argToCommand;

            public CommandSetupService(Assembly[] assemblies)
            {
                //Should validate that arg infos set delegate is not null
                var commandTypes = assemblies
                    .SelectMany(x => x.GetTypes())
                    .Where(type => type.GetInterfaces().Contains(typeof(ICommand<>)) && !type.IsInterface && !type.IsAbstract)
                    .ToList();
                _argToCommand = GetArgToCommandDictionary(commandTypes);
            }

            public void AddCommands(IServiceCollection sc)
                => _argToCommand.Select(x => x.Value).ToList().ForEach(c => sc.AddSingleton(sc));

            public void AddCommandCatalogue(IServiceCollection sc)
                => sc.AddSingleton<ICommandCatalogue>(new CommandCatalogue(_argToCommand));

            private Dictionary<Type, Type> GetArgToCommandDictionary(List<Type> commandTypes)
            {
                var argsToCommand = new Dictionary<Type, Type>();
                var errors = new List<string>();
                foreach (var commandType in commandTypes)
                {
                    try
                    {
                        var argsType = GetArgsType(commandType);
                        if (argsToCommand.ContainsKey(argsType))
                        {
                            errors.Add($"Multiple commands for args type {argsType.FullName} detected");
                            continue;
                        }

                        argsToCommand.Add(argsType, commandType);
                    }
                    catch (SimpleCLISetupException e)
                    {
                        errors.Add(e.Message);
                    }
                }

                if (errors.Any())
                    throw new SimpleCLISetupException(string.Join(Environment.NewLine, errors));


                return argsToCommand;
            }

            private Type GetArgsType(Type commandType)
            {
                var parsedArgsType = commandType
                    .GetInterface(typeof(ICommand<>).Name)
                    ?.GetGenericArguments()
                    .SingleOrDefault();

                if (parsedArgsType == null)
                    throw new SimpleCLISetupException(
                        $"Unable to get parsed args type for command {commandType.FullName} please check this is setup properly");

                return parsedArgsType;
            }
        }
    }
}