using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BaseCLI.ReflectionObjects
{
    internal class ArgInfoReflectionObject
    {
        private readonly object _object;

        public ArgInfoReflectionObject(object? @object)
        {
            if (@object == null || !IsArgInfo(@object))
                throw new InvalidReflectionObject();

            _object = @object;
        }

        public void Validate(List<string> args)
            => Execute(nameof(ArgInfo<object>.Validate), args);
        
        public string GetHelp()
            => (string)Execute(nameof(ArgInfo<object>.GetHelp));

        public void Parse<TArgs>(TArgs parsedArgs, List<string> args)
            => Execute(nameof(ArgInfo<object>.Parse), parsedArgs, args);

        private object Execute(string name, params object[] arguments)
            => _object.GetType().GetRuntimeMethods().Single(x => x.Name == name).Invoke(_object, arguments);

        private bool IsArgInfo(object @object)
            => GetGenericInheritanceHierarchy(@object.GetType()).Contains(typeof(ArgInfo<>));
        

        private IEnumerable<Type> GetGenericInheritanceHierarchy(Type type)
        {
            for (var current = type; current != null; current = current.BaseType)
                yield return current.IsGenericType ? current.GetGenericTypeDefinition() : current;
        }
    }
}