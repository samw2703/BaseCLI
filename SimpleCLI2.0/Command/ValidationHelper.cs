using System.Collections.Generic;
using System.Linq;
using SimpleCLI.Extensions;
using SimpleCLI.Validation;

namespace SimpleCLI.Command
{
	internal static class ValidationHelper
	{
		public static void ValidateMandatoryArgIsPresent(List<string> args, ArgInfo argInfo)
		{
			if (args.All(x => x != $"-{argInfo.Flag}") && argInfo.Mandatory)
				throw new ArgValidatorException($"No value supplied for mandatory argument {argInfo.FriendlyName} (-${argInfo.Flag})");
		}

		public static void ValidateSingleArgument(List<string> args, ArgInfo argInfo)
		{
			if (args.Count(x => x == $"-{argInfo.Flag}") > 1)
				throw new ArgValidatorException($"Multiple {argInfo.FriendlyName} (-${argInfo.Flag}) args found. You must only provide one of these");
		} 

		public static void ValidateEnoughArgsForThereToBeValue(List<string> args, ArgInfo argInfo)
		{
			var valueIndexes = args
				.FindAllIndexes($"-{argInfo.Flag}")
				.Select(x => x + 1);
			foreach (var valueIndex in valueIndexes)
			{
				if (valueIndex >= args.Count)
					throw new ArgValidatorException($"No value for {argInfo.FriendlyName} (-${argInfo.Flag}) was supplied");
			}
		}

		public static void RemoveKeysAndValuesFromArgs(List<string> args, ArgInfo argInfo)
		{
			var indexes = args
				.FindAllIndexes($"-{argInfo.Flag}")
				.GetIndexesAndIncrementedIndexes();

			RemoveFromArgsAt(indexes, args);
		}

		public static void RemoveKeysFromArgs(List<string> args, ArgInfo argInfo)
		{
			var indexes = args
				.FindAllIndexes($"-{argInfo.Flag}");

			RemoveFromArgsAt(indexes, args);
		}

		public static void ValidateValuesAreIntegers(List<string> args, ArgInfo argInfo)
		{
			var items = args.FindAllValues($"-{argInfo.Flag}");
			foreach (var item in items)
			{
				var valid = int.TryParse(item, out int _);
				if (!valid)
					throw new ArgValidatorException($"\"{item}\" is not a valid integer");
			}
		}

		private static void RemoveFromArgsAt(List<int> indexes, List<string> args)
		{
			foreach (var index in indexes.OrderByDescending(x => x))
				args.RemoveAt(index);
		}
	}
}