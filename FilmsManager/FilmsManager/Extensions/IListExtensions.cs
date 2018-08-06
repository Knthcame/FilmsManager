using Models.Classes;
using System.Collections.Generic;

namespace FilmsManager.Extensions
{
	public static class IListExtensions
	{
		public static void AddRange<TEntity>(this IList<TEntity> list, IList<TEntity> newList) where TEntity : IEntity
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
