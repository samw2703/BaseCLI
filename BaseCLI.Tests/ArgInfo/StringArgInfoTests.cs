﻿using System.Collections.Generic;
using BaseCLI.Validation;
using NUnit.Framework;

namespace BaseCLI.Tests.ArgInfo
{
	public class StringArgInfoTests
	{
		private const string ArgFlag = "test";
		private const string FriendlyName = "test flag";
		private readonly string _dashedFlag = $"-{ArgFlag}";

		[Test]
		public void Parse_SetsPropertyAsExpected()
		{
			const string flagValue = "PleaseWork";
			var parsedArgs = new TestParsedArgs();
			var argInfo = new StringArgInfo<TestParsedArgs>(ArgFlag, "", "Test");
			argInfo.Parse(parsedArgs, new List<string>
			{
				_dashedFlag,
				flagValue
			});

			Assert.AreEqual(flagValue, parsedArgs.Test);
		}

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
		public void Validate_ExactlyOneOfArgTypePassedIn_ArgsHasFlagAndSubsequentArgRemoved()
		{
			const string toBeRemovedArg = "ToBeRemoved";
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
		public void Validate_MandatoryArgIsMissing_Throws()
		{
			var args = new List<string>();
			var argsCopy = CopyList(args);
			Assert.Throws<ArgValidatorException>(() => Validate(args, true));

			AssertHelpers.AreEquivalent(argsCopy, args);
		}

		private List<string> CopyList(List<string> args)
			=> new List<string>(args);


		private void Validate(List<string> args, bool mandatory = false)
		{
			new StringArgInfo<TestParsedArgs>(ArgFlag, FriendlyName, "", mandatory).Validate(args);
		}

		private class TestParsedArgs
		{
            public string Test { get; set; }
		}
	}
}
