﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleCLI.Command;
using SimpleCLI.Execution;
using SimpleCLI.Help;
using SimpleCLI.Validation;

namespace SimpleCLI.Conductor
{
	internal class CommandConductor
	{
		private readonly ICommandFactory _commandFactory;
		private readonly IValidator _validator;
		private readonly CommandAndArgsParser _commandAndArgsParser;
		private readonly Helper _helper;
		private readonly IConsole _console;
		private readonly ICommandExecutor _commandExecutor;

		public CommandConductor(ICommandFactory commandFactory,
			IValidator validator,
			CommandAndArgsParser commandAndArgsParser,
			Helper helper,
			IConsole console,
			ICommandExecutor commandExecutor)
		{
			_commandFactory = commandFactory;
			_validator = validator;
			_commandAndArgsParser = commandAndArgsParser;
			_helper = helper;
			_console = console;
			_commandExecutor = commandExecutor;
		}

		public void Conduce(string[] args)
		{
			var commandAndArgs = _commandAndArgsParser.Parse(args);
			var command = _commandFactory.GetCommand(commandAndArgs.CommandName);
			var needsHelp = ArgsContainsHelpFlag(commandAndArgs.Args);
			if (command == null)
			{
				PrintHelp();
				return;
			}
			if (needsHelp)
			{
				PrintHelp(command);
				return;
			}

			const string genericErrorMessage = @"Something went wrong, please check the arguments you passed in are correct

{0}";
			try
			{
				Validate(new List<string>(commandAndArgs.Args), command.ArgInfos);
			}
			catch (ArgValidatorException e)
			{
				var message = @$"{e.Message}

{_helper.GetCommandHelp(command)}";
				_console.WriteLine(message);
				return;
			}
			catch (Exception)
			{
				_console.WriteLine(string.Format(genericErrorMessage, _helper.GetCommandHelp(command)));
				return;
			}

			try
			{
				_commandExecutor.Execute(command, commandAndArgs.Args);
			}
			catch (Exception)
			{
				_console.WriteLine(string.Format(genericErrorMessage, _helper.GetCommandHelp(command)));
			}
		}

		private void PrintHelp(ICommand command = null)
		{
			if (command == null)
			{
				_console.WriteLine(_helper.GetToolHelp(Assembly.GetAssembly(GetType())));
				return;
			}

			_console.WriteLine(_helper.GetCommandHelp(command));
		}

		private bool ArgsContainsHelpFlag(List<string> args)
			=> args.Any(x => x == "-h");

		private void Validate(List<string> args, List<ArgInfo> argInfos)
			=> _validator.Validate(args, argInfos);
	}
}
