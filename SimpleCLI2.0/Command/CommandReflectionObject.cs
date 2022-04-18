using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCLI.Command
{
    internal class CommandReflectionObject
    {
		public List<ArgInfo> ArgInfos { get; }
		public string Name { get; }
		public string Description { get; }

		public CommandReflectionObject(object? @object)
        {
			if (@object == null || !@object.GetType().GetInterfaces().Any(x => x == typeof(ICommand<>)))
				throw new ArgumentException("Invalid CommandReflectionObject");

			ArgInfos = (List<ArgInfo>)@object.GetType().GetProperty(nameof(ICommand<object>.ArgInfos)).GetValue(@object);
			Name = (string)@object.GetType().GetProperty(nameof(ICommand<object>.Name)).GetValue(@object);
			Description = (string)@object.GetType().GetProperty(nameof(ICommand<object>.Description)).GetValue(@object);
		}
    }
}
