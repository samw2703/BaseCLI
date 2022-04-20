using System;

namespace BaseCLI
{
    internal class ArgInfoBuilderError : Exception
    {
        public ArgInfoBuilderError(string message)
            : base(message)
        {
        }
    }
}