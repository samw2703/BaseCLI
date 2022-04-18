using System;
using System.Collections.Generic;

namespace SimpleCLI.Command
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

        private object Execute(string name, params object[] arguments)
            => _object.GetType().GetMethod(name).Invoke(_object, arguments);
    }
}