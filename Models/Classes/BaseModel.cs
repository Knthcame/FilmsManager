using SQLite;

namespace Models.Classes
{
    public class BaseModel : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ApiId { get; set; }
    }
}