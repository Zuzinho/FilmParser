using System;

namespace FilmParser.Model
{
    internal class Cinema: ISqlConverter
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Cinema(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
        }

        public string GetVariablesString()
        {
            throw new NotImplementedException();
        }

        public string GetValuesString()
        {
            throw new NotImplementedException();
        }
    }
}
