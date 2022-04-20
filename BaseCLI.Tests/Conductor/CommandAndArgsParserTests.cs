using BaseCLI.Conductor;
using NUnit.Framework;

namespace BaseCLI.Tests.Conductor
{
	public class When_parsing
	{
		private static readonly CommandAndArgsParser _parser = new();
		public class and_input_args_length_is_zero
		{
			[Test]
			public void Returns_empty_command_and_empty_args_list()
			{
				var result = _parser.Parse(new string[0]);

				Assert.AreEqual("", result.CommandName);
				Assert.AreEqual(0, result.Args.Count);
			}
		}

		public class and_first_arg_is_a_flag
		{
			[Test]
			public void Returns_empty_command_and_full_list_of_args()
			{
				var args = new[] {"-flag", "something", "else"};
				var result = _parser.Parse(args);

				Assert.AreEqual("", result.CommandName);
				Assert.AreEqual(args.Length, result.Args.Count);
			}
		}

		public class and_first_arg_is_not_a_flag
		{
			[Test]
			public void Returns_first_arg_as_command_name_and_remainder_as_args()
			{
				var args = new[] { "text", "other text", "-flag", "something", "else" };
				var result = _parser.Parse(args);

				Assert.AreEqual("text", result.CommandName);
				Assert.AreEqual(args.Length - 1, result.Args.Count);
			}
		}

		public class and_none_of_the_args_are_flags
		{
			[Test]
			public void Returns_first_arg_as_command_name_and_remainder_as_args()
			{
				var args = new[] { "text", "other text", "something", "else" };
				var result = _parser.Parse(args);

				Assert.AreEqual("text", result.CommandName);
				Assert.AreEqual(args.Length - 1, result.Args.Count);
			}
		}
	}
}