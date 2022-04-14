using System.Collections.Generic;
using NUnit.Framework;
using SimpleCLI.Command;
using SimpleCLI.Validation;

namespace SimpleCLI.Tests.Validation
{
	public class ValidatorTests
	{
		[Test]
		public void Validate_SoThatThereAreSomeArgsLeftAtTheEnd_Throws()
		{
			var args = new List<string> {"We're", "going", "Nowhere"};
			var validator = new Validator();
			Assert.Throws<ArgValidatorException>(() => validator.Validate(args, new List<global::SimpleCLI.Command.ArgInfo>()));
		}

		[Test]
		public void Validate_SoThatThereAreNoArgsLeftAtTheEnd_DoesNotThrow()
		{
			var args = new List<string> { "-test", "I'll Leave" };
			var argInfos = new List<global::SimpleCLI.Command.ArgInfo>
			{
				new StringArgInfo("test", "")
			};
			var validator = new Validator();
			Assert.DoesNotThrow(() => validator.Validate(args, argInfos));
		}
	}
}