using System.Collections.Generic;
using System.Linq;

namespace BaseCLI
{
    public static class ArgInfoUtilities
    {
        public static List<string> GetValuesForFlag(this List<string> strings, string flag)
            => strings
                .FindAllIndexes($"-{flag}")
                .Select(x => strings[x + 1])
                .ToList();

        public static List<int> FindAllIndexes(this List<string> list, string str)
        {
            var indexes = new List<int>();
            for (int idx = 0; idx < list.Count; idx++)
            {
                if (list[idx] == str)
                    indexes.Add(idx);
            }

            return indexes;
        }

        public static List<string> FindAllValues(this List<string> list, string str)
        {
            var indexes = list.FindAllIndexes(str);

            return indexes.Select(x => list[x + 1]).ToList();
        }

		public static List<int> GetIndexesAndIncrementedIndexes(this List<int> list)
        {
            var newList = list.Select(x => x + 1).ToList();
            newList.AddRange(list);

            return newList;
        }
	}
}