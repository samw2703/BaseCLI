using Newtonsoft.Json;
using NUnit.Framework;

namespace SimpleCLI.Tests
{
	public static class AssertHelpers
	{
		public static void AreEquivalent( object expectedObj, object actualObj)
		{
			Assert.AreEqual(JsonConvert.SerializeObject(expectedObj), JsonConvert.SerializeObject(actualObj));
		}
	}
}