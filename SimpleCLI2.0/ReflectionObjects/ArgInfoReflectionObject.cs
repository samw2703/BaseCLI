using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleCLI.ReflectionObjects
{
    internal class ArgInfoReflectionObject
    {
        private readonly object _object;

        public ArgInfoReflectionObject(object? @object)
        {
            //Also need to check that the object is indeed an ArgInfo
            if (@object == null)
                throw new ArgumentException("Invalid CommandReflectionObject");

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
    }
}