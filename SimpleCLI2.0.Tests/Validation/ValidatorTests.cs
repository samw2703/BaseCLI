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
			Assert.Throws<ArgValidatorException>(() => validator.Validate(args, new List<ArgInfoReflectionObject>()));
		}

		[Test]
		public void Validate_SoThatThereAreNoArgsLeftAtTheEnd_DoesNotThrow()
		{
			var args = new List<string> { "-test", "I'll Leave" };
			var argInfos = new List<ArgInfoReflectionObject>
			{
				new(new StringArgInfo<object>("test", "", ""))
			};
			var validator = new Validator();
			Assert.DoesNotThrow(() => validator.Validate(args, argInfos));
		}
	}
}