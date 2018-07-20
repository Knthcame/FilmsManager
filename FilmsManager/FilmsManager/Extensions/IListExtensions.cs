using Models.Classes;
using System.Collections.Generic;

namespace FilmsManager.Extensions
{
	public static class IListExtensions
	{
		public static void AddRange(this IList<MovieModel> list, IList<MovieModel> newList)
		{
			foreach (var item in newList)
			{
				list.Add(item);
			}
		}
	}
}
