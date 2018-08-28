using SQLite;

namespace Models.Classes
{
    public class BaseModel : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}