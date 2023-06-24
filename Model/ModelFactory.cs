using System;
using System.Data.SqlClient;

namespace FilmParser.Model
{
    internal static class ModelFactory
    {
        public static ISqlConverter GetModelObject<T>(SqlDataReader reader)
        {
            Type type = typeof(T);
            if (type == typeof(Cinema)) return new Cinema(reader);
            if (type == typeof(Film)) return new Film(reader);
            if (type == typeof(Session)) return new Session(reader);

            return null;
        }
    }
}
