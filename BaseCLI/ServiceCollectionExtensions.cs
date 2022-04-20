using BaseCLI.Conductor;
using BaseCLI.Execution;
using BaseCLI.Help;
using BaseCLI.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCLI
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddCliServices(this IServiceCollection sc)
        {
            sc.AddSingleton<ICommandExecutor, CommandExecutor>();
            sc.AddSingleton<IFlagParser, FlagParser>();
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