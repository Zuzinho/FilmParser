using System;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

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

        public Film(SqlDataReader reader)
        {
            FilmId = (int)(reader.GetValue(0));
            Name = reader.GetString(1);
            Genre = reader.GetString(2);
            Description = reader.GetString(3);
            AvatarPath = reader.GetString(4);
        }

        public string GetTableName()
        {
            return "Films";
        }

        public string GetValuesString()
        {
            return "(Name, Genre, Description, AvatarPath) VALUES " +
                $"('{Name}', '{Genre}', '{Description}', '{AvatarPath}')";
        }

        public string GetSetString()
        {
            return $"Name = '{Name}', Genre = '{Genre}', Description = '{Description}', AvatarPath = '{AvatarPath}'";
        }

        public string GetIdCondition()
        {
            return $"FilmId = {FilmId}";
        }
    }
}
