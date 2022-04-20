using System;

namespace SimpleCLI.ArgInfoBuilder
{
    internal class ArgInfoBuilderError : Exception
    {
        public ArgInfoBuilderError(string message)
            : base(message)
        {
        }
    }
}