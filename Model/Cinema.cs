using System;
using System.Data.SqlClient;

namespace FilmParser.Model
{
    internal class Cinema: ISqlConverter
    {
        public int CinemaId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Cinema(int cinemaId, string name, string address)
        {
            CinemaId = cinemaId;
            Name = name;
            Address = address;
        }

        public Cinema(SqlDataReader reader)
        {
            CinemaId = (int)(reader.GetValue(0));
            Name = reader.GetString(1);
            Address = reader.GetString(2);
        }

        public string GetTableName()
        {
            return "Cinemas";
        }

        public string GetValuesString()
        {
            return "(Name, Address) VALUES " +
                $"('{Name}', '{Address}')";
        }

        public string GetSetString()
        {
            return $"SET Name = '{Name}', Address = '{Address}'";
        }

        public string GetIdCondition()
        {
            return $"CinemaId = {CinemaId}";
        }
    }
}
