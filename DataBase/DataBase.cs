using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System;
using FilmParser.Model;

namespace FilmParser.DataBase
{
    internal class DataBase
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
        
        protected static SqlConnection Connection = new SqlConnection(_connectionString);

        protected static Dictionary<Type, string> TableNamesPairs = new Dictionary<Type, string>()
        {
            {typeof(Cinema), "Cinemas" },
            {typeof(Film), "Films" },
            {typeof(Session), "Sessions" },
            {typeof(int), "TablesId" }
        };
    }
}
