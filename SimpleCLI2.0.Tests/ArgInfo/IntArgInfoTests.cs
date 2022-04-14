using System.Collections.Generic;
using NUnit.Framework;
using SimpleCLI.Command;
using SimpleCLI.Validation;

namespace SimpleCLI.Tests.ArgInfo
{
	public class IntArgInfoTests
	{
		private const string ArgFlag = "test";
		private const string FriendlyName = "test flag";
		private readonly string _dashedFlag = $"-{ArgFlag}";

		[Test]
		public void Validate_MoreThanOneOfThatArgTypePassedIn_Throws()
		{
			var args = new List<string> { _dashedFlag, "WillThrowError", _dashedFlag, "SeriouslyI'llThrow" };
			Assert.Throws<ArgValidatorException>(() => Validate(args));
		}

		[Test]
		public void Validate_NoneOfThatArgTypeExistInArgs_ArgsRemainsUnchanged()
		{
			var args = new List<string> { "We're", "Just", "a", "bunch", "of", "stuff" };
			var argsCopy = CopyList(args);
			Validate(args);

			AssertHelpers.AreEquivalent(argsCopy, args);
		}

		[Test]
		public void Validate_ExactlyOneOfArgTypePassedInButIsNotInt_Throws()
		{
			const string invalidArg = "StringsAren'tAllowedHere";
			var args = new List<string> { "We're", "Just", "a", _dashedFlag, invalidArg, "bunch", "of", "stuff" };
			Assert.Throws<ArgValidatorException>(() => Validate(args));
		}

		[Test]
		public void Validate_ExactlyOneOfValidArgTypePassedIn_ArgsHasFlagAndSubsequentArgRemoved()
		{
			const string toBeRemovedArg = "298";
			var args = new List<string> { "We're", "Just", "a", _dashedFlag, toBeRemovedArg, "bunch", "of", "stuff" };
			Validate(args);

			Assert.False(args.Contains(_dashedFlag));
			Assert.False(args.Contains(toBeRemovedArg));
		}

		[Test]
		public void Validate_ArgsContainsJustFlagAndNoOtherArgs_Throws()
		{
			var args = new List<string> { _dashedFlag };
			Assert.Throws<ArgValidatorException>(() => Validate(args));
		}

		[Test]
		public void Validate_ArgsContainsOneArgWhichIsNotFlag_ArgsRemainsUnchanged()
		{
			var args = new List<string> { "OnlyArg" };
			var argsCopy = CopyList(args);
			Validate(args);

			AssertHelpers.AreEquivalent(argsCopy, args);
		}

		[Test]
		public void Validate_FlagIsMandatoryAndIsMissing_Throws()
		{
			var args = new List<string>();
			Assert.Throws<ArgValidatorException>(() => Validate(args, true));
		}

		[Test]
		public void Parse_SetsPropertyAsExpected()
		{
			const int flagValue = 259;
			var parsedArgs = new TestParsedArgs();
			var argInfo = new IntArgInfo(ArgFlag, FriendlyName);
			argInfo.Parse(parsedArgs, new List<string>
			{
				_dashedFlag,
				flagValue.ToString()
			});

			Assert.AreEqual(flagValue, parsedArgs.Test);
		}

		private class TestParsedArgs : ParsedArgs
		{
			[Flag("test")]
			public int Test { get; set; }
		}

		private List<string> CopyList(List<string> args)
			=> new(args);

		private void Validate(List<string> args, bool mandatory = false)
		{
			new IntArgInfo(ArgFlag, FriendlyName, mandatory)
				.Validate(args);
		}
	}
}