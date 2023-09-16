using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BaseCLI.ReflectionObjects
{
    internal class CommandReflectionObject
    {
        private readonly object _object;

		public List<ArgInfoReflectionObject> ArgInfos { get; }
		public string Name { get; }
		public string Description { get; }
        public Type CommandType { get; }

		public CommandReflectionObject(object? @object)
        {
            if (@object == null || @object.GetType().GetInterface(typeof(ICommand<>).Name) == null)
				throw new InvalidReflectionObject();

            ArgInfos = GetArgInfos(@object).ToList();
			Name = (string)@object.GetType().GetProperty(nameof(ICommand<object>.Name)).GetValue(@object);
			Description = (string)@object.GetType().GetProperty(nameof(ICommand<object>.Description)).GetValue(@object);
            CommandType = @object.GetType();
            _object = @object;
        }

        public async Task Execute<TArgs>(TArgs args)
        {
            var task = (Task)_object.GetType().GetRuntimeMethods()
                .Single(x => x.Name == nameof(ICommand<object>.Execute))
                .Invoke(_object, new object[] { args });
            await task.ConfigureAwait(false);
        }

        private IEnumerable<ArgInfoReflectionObject> GetArgInfos(object @object)
        {
            var enumerable = (IEnumerable)@object.GetType().GetProperty(nameof(ICommand<object>.ArgInfoCollection)).GetValue(@object);
            foreach (var item in enumerable)
                yield return new ArgInfoReflectionObject(item);
        }
    }
}
