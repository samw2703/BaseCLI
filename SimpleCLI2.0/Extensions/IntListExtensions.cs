using System.Collections.Generic;
using System.Linq;

namespace SimpleCLI.Extensions
{
	internal static class IntListExtensions
	{
		public static List<int> GetIndexesAndIncrementedIndexes(this List<int> list)
		{
			var newList = list.Select(x => x + 1).ToList();
			newList.AddRange(list);

			return newList;
		}

		public static List<int> GetIncrementedIndexes(this List<int> list)
		{
			return list.Select(x => x + 1).ToList();
		}
	}
}