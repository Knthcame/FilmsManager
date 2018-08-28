using Models.Classes;
using System.Collections.Generic;

namespace FilmsManager.Extensions
{
    public static class IListExtensions
    {
        public static void AddRange<TEntity>(this IList<TEntity> list, IList<TEntity> newList) where TEntity : IEntity
        {
            if (list == null || newList == null)
                return;

            foreach (var item in newList)
            {
                list.Add(item);
            }
        }

        public static bool GetGenre(this IList<GenreModel> list, int id, out GenreModel genre)
        {
            genre = null;

            if (list == null)
                return false;

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
