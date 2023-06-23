namespace FilmParser.Model
{
    internal class Film: ISqlConverter
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Genre { get; private set; }
        public string Description { get; private set; }
        public string AvatarPath { get; private set; }

        public Film(int id, string name, string genre, string description, string avatarPath)
        {
            Id = id;
            Name = name;
            Genre = genre;
            Description = description;
            AvatarPath = avatarPath;
        }

        public string GetTableName()
        {
            throw new System.NotImplementedException();
        }

        public string GetVariablesString()
        {
            throw new System.NotImplementedException();
        }

        public string GetValuesString()
        {
            throw new System.NotImplementedException();
        }
    }
}
