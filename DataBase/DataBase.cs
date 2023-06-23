using System.Data.SqlClient;

namespace FilmParser.DataBase
{
    internal class DataBase
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\source\\repos\\FilmParser\\FilmParser\\AppData\\CinemaDB.mdf;Integrated Security=True";
        protected SqlConnection connection => new SqlConnection(connectionString);

        
    }
}
