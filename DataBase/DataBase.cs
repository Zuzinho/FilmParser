using System.Data.SqlClient;
using System.Configuration;

namespace FilmParser.DataBase
{
    internal class DataBase
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
        protected static SqlConnection Connection = new SqlConnection(_connectionString);

        
    }
}
