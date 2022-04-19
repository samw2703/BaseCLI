using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimpleCLI.Validation;

namespace SimpleCLI.Tests.ArgInfo
{
	public class StringCollectionArgInfoTests
	{
		private const string Flag = "test";
		private const string FriendlyName = "String collection flag";
		private const string PropertyName = "Test";
		private string _dashedFlag = $"-{Flag}";

		[Test]
		public void Parse_SingleFlagPassedIn_SetsPropertyAsListWithOneValue()
		{
			const string flagValue = "PleaseWork";
			var parsedArgs = new TestParsedArgs();
			var args = new List<string>
			{
				_dashedFlag,
				flagValue,
				"Something",
			};
			Parse(args, parsedArgs);

			Assert.AreEqual(1, parsedArgs.Test.Count);
			Assert.True(parsedArgs.Test.Contains(flagValue));
		}

		[Test]
		public void Parse_MultipleFlagsPassedIn_SetsPropertyAsListWithAllValues()
		{
			const string flagValue1 = "PleaseWork";
			const string flagValue2 = "I'm begging you";
			var parsedArgs = new TestParsedArgs();
			var args = new List<string>
			{
				_dashedFlag,
				flagValue1,
				"Something",
				_dashedFlag,
				flagValue2
			};
			Parse(args, parsedArgs);

			Assert.AreEqual(2, parsedArgs.Test.Count);
			Assert.True(parsedArgs.Test.Contains(flagValue1));
			Assert.True(parsedArgs.Test.Contains(flagValue2));
		}

		[Test]
		public void Parse_NoFlagsPassedIn_SetsPropertyAsEmptyList()
		{
			var parsedArgs = new TestParsedArgs();
			var args = new List<string>
			{
				"Something"
			};
			Parse(args, parsedArgs);

			Assert.False(parsedArgs.Test.Any());
		}

		[Test]
		public void Validate_FlagIsMandatoryAndNoArgs_Throws()
		{
			Assert.Throws<ArgValidatorException>(() => Validate(new List<string>(), true));
		}

		[Test]
		public void Validate_FlagIsNotMandatoryAndNoArgs_ArgsRemainUnchanged()
		{
			var args = new List<string>
			{
				"Something"
			};
			Validate(args);

			Assert.AreEqual(1, args.Count);
		}

		[Test]
		public void Validate_ArgsArePassedIn_KeyAndValuesAreRemoved()
		{
			const string otherArg = "Something";
			var args = new List<string>
			{
				_dashedFlag,
				"Flag Value 1",
				otherArg,
				_dashedFlag,
				"Flag Value 2"
			};
			Validate(args);

			Assert.AreEqual(otherArg, args.Single());
		}

		[Test]
		public void Validate_ValueIsMissingForFlag_Throws()
		{
			const string otherArg = "Something";
			var args = new List<string>
			{
				_dashedFlag,
				"Flag Value 1",
				otherArg,
				_dashedFlag
			};

			Assert.Throws<ArgValidatorException>(() => Validate(args));
		}

		private void Validate(List<string> args, bool mandatory = false)
		{
			new StringCollectionArgInfo<TestParsedArgs>(Flag, FriendlyName, PropertyName, mandatory)
				.Validate(args);
		}

		private void Parse(List<string> args, TestParsedArgs parsedArgs)
		{
			new StringCollectionArgInfo<TestParsedArgs>(Flag, FriendlyName, PropertyName)
				.Parse(parsedArgs, args);
		}

		private class TestParsedArgs
		{
            public List<string> Test { get; set; }
		}
	}
}