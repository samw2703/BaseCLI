using System.Collections.Generic;
using System.Linq;

namespace SimpleCLI.Extensions
{
	internal static class ListExtensions
	{
		public static List<T> GetItemsAtIndexes<T>(this List<T> list, List<int> indexes)
			=> indexes
				.Select(index => list[index])
				.ToList();
	}
}