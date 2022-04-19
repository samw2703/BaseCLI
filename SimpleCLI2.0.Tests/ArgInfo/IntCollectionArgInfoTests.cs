using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimpleCLI.Command;
using SimpleCLI.Validation;

namespace SimpleCLI.Tests.ArgInfo
{
	public class IntCollectionArgInfoTests
	{
		private const string Flag = "test";
		private const string FriendlyName = "Lots of integers";
		private const string PropertyName = "Test";
		private string _dashedFlag = $"-{Flag}";

		[Test]
		public void Parse_SingleFlagPassedIn_SetsPropertyAsListWithOneValue()
		{
			const int flagValue = 20;
			var parsedArgs = new TestParsedArgs();
			var args = new List<string>
			{
				_dashedFlag,
				flagValue.ToString(),
				"Something",
			};
			Parse(args, parsedArgs);

			Assert.AreEqual(1, parsedArgs.Test.Count);
			Assert.True(parsedArgs.Test.Contains(flagValue));
		}

		[Test]
		public void Parse_MultipleFlagsPassedIn_SetsPropertyAsListWithAllValues()
		{
			const int flagValue1 = 49;
			const int flagValue2 = 16843;
			var parsedArgs = new TestParsedArgs();
			var args = new List<string>
			{
				_dashedFlag,
				flagValue1.ToString(),
				"Something",
				_dashedFlag,
				flagValue2.ToString()
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
				"1",
				otherArg,
				_dashedFlag,
				"2"
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
				"1",
				otherArg,
				_dashedFlag
			};

			Assert.Throws<ArgValidatorException>(() => Validate(args));
		}

		[Test]
		public void Validate_ValueIsNotAValidInteger_Throws()
		{
			const string otherArg = "Something";
			var args = new List<string>
			{
				_dashedFlag,
				"Not an integer",
				otherArg,
			};

			Assert.Throws<ArgValidatorException>(() => Validate(args));
		}

		private void Validate(List<string> args, bool mandatory = false)
		{
			new IntCollectionArgInfo<TestParsedArgs>(Flag, FriendlyName, PropertyName, mandatory)
				.Validate(args);
		}

		private void Parse(List<string> args, TestParsedArgs parsedArgs)
		{
			new IntCollectionArgInfo<TestParsedArgs>(Flag, FriendlyName, PropertyName)
				.Parse(parsedArgs, args);
		}

		private class TestParsedArgs
		{
            public List<int> Test { get; set; }
		}
	}
}