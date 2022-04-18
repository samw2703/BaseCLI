using System.Linq;
using System.Reflection;

namespace SimpleCLI.Command
{
	internal static class ParserHelper
	{

		public static void SetPropertyValue<TArgs>(TArgs parsedArgs, string flag, object value)
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