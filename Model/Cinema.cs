using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace FilmParser.Model
{
    internal class Cinema: ISqlConverter
    {
        private readonly int _id;

        public int Id => _id;

        public string Name { get; private set; }
        public string Address { get; private set; }


        public Cinema(int cinemaId, string name, string address = "")
        {
            _id = cinemaId;
            Name = name;
            Address = address;
        }

        public Cinema(SqlDataReader reader)
        {
            _id = (int)(reader.GetValue(0));
            Name = reader.GetString(1);
            Address = reader.GetString(2);
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
    }
}
