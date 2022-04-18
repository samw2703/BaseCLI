using Microsoft.Extensions.DependencyInjection;
using SimpleCLI.Conductor;
using SimpleCLI.Execution;
using SimpleCLI.FlagParsing;
using SimpleCLI.Help;
using SimpleCLI.Validation;

namespace SimpleCLI
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddCliServices(this IServiceCollection sc)
        {
            sc.AddSingleton<ICommandExecutor, CommandExecutor>();
            sc.AddSingleton<FlagParser>();
            sc.AddSingleton<ICommandFactory, CommandFactory>();
            sc.AddSingleton<CommandAndArgsParser>();
            sc.AddSingleton<Helper>();
            sc.AddSingleton<IValidator, Validator>();
            sc.AddSingleton<ICommandFactory, CommandFactory>();
            sc.AddSingleton<IConsole, Console>();
            sc.AddSingleton<CommandConductor>();
        }
    }
}