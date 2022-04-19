using System.Linq;
using System.Reflection;

namespace SimpleCLI
{
	internal static class ArgInfoParserHelper
	{
        public static void SetPropertyValue<TArgs>(TArgs parsedArgs, string name, object value)
        {
            GetProperty(parsedArgs, name).SetValue(parsedArgs, value);
        }

        private static PropertyInfo GetProperty<TArgs>(TArgs args, string name)
            => args.GetType().GetRuntimeProperties().SingleOrDefault(x => x.Name == name);
    }
}