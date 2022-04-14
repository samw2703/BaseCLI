using System.Linq;
using System.Reflection;

namespace SimpleCLI.Command
{
	internal static class ParserHelper
	{

		public static void SetPropertyValue<TParsedArgs>(TParsedArgs parsedArgs, string flag, object value) where TParsedArgs : ParsedArgs
		{
			var property = parsedArgs
				.GetType()
				.GetProperties()
				.Single(x => IsPropertyForFlag(x, flag));

			property.SetValue(parsedArgs, value);
		}

		private static bool IsPropertyForFlag(PropertyInfo propertyInfo, string flag)
		{
			return (propertyInfo
				.GetCustomAttribute<FlagAttribute>()
				?.Flag ?? null) == flag;
		}
	}
}