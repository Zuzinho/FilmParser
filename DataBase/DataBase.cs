using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System;
using FilmParser.Model;
using System.Threading.Tasks;

namespace FilmParser.Database
{
    internal class DataBase
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["CinemaDB"].ConnectionString;
        
        protected static SqlConnection Connection = new SqlConnection(_connectionString);

        protected static Dictionary<Type, string> TableNamesPairs = new Dictionary<Type, string>()
        {
            {typeof(Cinema), "Cinemas" },
            {typeof(Film), "Films" },
            {typeof(Session), "Sessions" }        
        };

        public static void OpenConnection()
        {
            Connection.Open();
        }

        public static void CloseConnection()
        {
            Connection.Close();
        }

        protected async static Task ExecuteNonQueryAsync(string sqlString)
        {
            using (SqlCommand sqlCommand = new SqlCommand(sqlString, Connection))
            {
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        protected static void ExecuteNonQuery(string sqlString)
        {
            using (SqlCommand sqlCommand = new SqlCommand(sqlString, Connection))
            {
                sqlCommand.ExecuteNonQuery();
            }
        }

        protected async static Task<SqlDataReader> GetReaderAsync(string sqlString)
        {
            SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
            return await sqlCommand.ExecuteReaderAsync();
        }

        protected static SqlDataReader GetReader(string sqlString)
        {
            SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
            return sqlCommand.ExecuteReader();
        }

        protected async static Task<string> GetStringValueAsync(string sqlString)
        {
            using (var reader = await GetReaderAsync(sqlString))
            {
                return await reader.ReadAsync() ? reader.GetString(0) : null;
            }
        }

        protected async static Task<int> GetIntValueAsync(string sqlString)
        {
            using (var reader = await GetReaderAsync(sqlString))
            {
                return await reader.ReadAsync() ? Convert.ToInt32(reader.GetValue(0)) : -1;
            }
        }
    }
}
