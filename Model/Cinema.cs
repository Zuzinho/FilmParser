using System;

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
