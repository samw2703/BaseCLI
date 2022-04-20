using System;

namespace SimpleCLI
{
    internal class ArgInfoBuilderError : Exception
    {
        public ArgInfoBuilderError(string message)
            : base(message)
        {
        }
    }
}