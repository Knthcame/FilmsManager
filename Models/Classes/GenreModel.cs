namespace Models.Classes
{
    public class GenreModel : BaseModel
    {
        public string Name { get; set; }

        public GenreModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public GenreModel() { }
    }
}
