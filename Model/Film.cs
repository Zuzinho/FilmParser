using System;

namespace FilmParser.Model
{
    internal class Film: ISqlConverter
    {
        public int FilmId { get; private set; }
        public string Name { get; private set; }
        public string Genre { get; private set; }
        public string Description { get; private set; }
        public string AvatarPath { get; private set; }

        public Film(int filmId, string name, string genre, string description, string avatarPath)
        {
            FilmId = filmId;
            Name = name;
            Genre = genre;
            Description = description;
            AvatarPath = avatarPath;
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
        }

        public string GetValuesString()
        {
            throw new NotImplementedException();
        }

        public string GetSetString()
        {
            throw new NotImplementedException();
        }

        public string GetIdCondition()
        {
            throw new NotImplementedException();
        }
    }
}
