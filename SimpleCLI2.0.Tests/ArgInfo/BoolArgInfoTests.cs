using System.Collections.Generic;
using NUnit.Framework;
using SimpleCLI.Command;
using SimpleCLI.Validation;

namespace SimpleCLI.Tests.ArgInfo
{
	public class BoolArgInfoTests
	{
		private const string Flag = "bool";
		private const string FriendlyName = "bool";
		private string _dashedFlag = $"-{Flag}";

		[Test]
		public void Validate_ArgsContainsSingleFlag_RemovesArgFromListOfFlags()
		{
			var args = new List<string>
			{
				_dashedFlag,
				"something else"
			};
			Validate(args);

			Assert.False(args.Contains(_dashedFlag));
			Assert.True(args.Count == 1);
		}

		[Test]
		public void Validate_ArgsDoesNotContainFlag_ArgsRemainUnchanged()
		{
			var args = new List<string>
			{
				"something else"
			};
			Validate(args);

			Assert.True(args.Count == 1);
		}

		[Test]
		public void Validate_ArgsContainsMultipleOfFlag_Throws()
		{
			var args = new List<string>
			{
				_dashedFlag,
				"something else",
				_dashedFlag

			};
			Assert.Throws<ArgValidatorException>(() => Validate(args));
		}

		[Test]
		public void Parse_FlagIsPresent_SetPropertyTrue()
		{
			var parsedArgs = new TestParsedArgs();
			var args = new List<string>
			{
				_dashedFlag
			};
			Parse(args, parsedArgs);

			Assert.True(parsedArgs.Test);
		}

		[Test]
		public void Parse_FlagIsNotPresent_SetPropertyFalse()
		{
			var parsedArgs = new TestParsedArgs();
			Parse(new List<string>(), parsedArgs);

			Assert.False(parsedArgs.Test);
		}

		private void Parse(List<string> args, TestParsedArgs parsedArgs)
		{
			new BoolArgInfo<TestParsedArgs>(Flag, FriendlyName, "")
				.Parse(parsedArgs, args);
		}

		private void Validate(List<string> args)
		{
			new BoolArgInfo<TestParsedArgs>(Flag, FriendlyName, "")
				.Validate(args);
		}

		private class TestParsedArgs
		{
			[Flag("bool")]
			public bool Test { get; set; }
		}
	}
}