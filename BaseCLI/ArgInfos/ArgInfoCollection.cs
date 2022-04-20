using System.Collections;
using System.Collections.Generic;

namespace BaseCLI
{
    public class ArgInfoCollection<TArgs> : IEnumerable where TArgs : new()
    {
        private readonly List<ArgInfo<TArgs>> _argInfos;

        internal ArgInfoCollection(List<ArgInfo<TArgs>> argInfos)
        {
            _argInfos = argInfos;
        }

        IEnumerator IEnumerable.GetEnumerator() => _argInfos.GetEnumerator();
    }
}