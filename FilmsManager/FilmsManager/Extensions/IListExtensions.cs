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

        public static bool GetGenre(this IList<GenreModel> list, string id, out GenreModel genre)
        {
            genre = null;
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    genre = item;
                    return true;
                }
            }
            return false;
        }
	}
}
