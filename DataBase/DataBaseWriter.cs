using FilmParser.Model;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace FilmParser.DataBase
{
    internal class DataBaseWriter : DataBase
    {
        public static void InsertData<T>(T modelObject) where T : ISqlConverter
        {
            string sqlString = $"INSERT INTO {ModelIdentifier.GetTableName<T>()} {modelObject.GetValuesString()}";

            ExecuteNonQuery(sqlString);
        }

        public static void UpdateData<T>(T modelObject) where T : ISqlConverter
        {
            string sqlString = $"UPDATE {ModelIdentifier.GetTableName<T>()} {modelObject.GetSetString()} " +
                $"WHERE Id = {modelObject.Id}";

            ExecuteNonQuery(sqlString);
        }

        public static void DeleteData<T>(T modelObject) where T : ISqlConverter
        {
            string sqlString = $"DELETE FROM {ModelIdentifier.GetTableName<T>()}" +
                $"WHERE Id = {modelObject.Id}";

            ExecuteNonQuery(sqlString);

            if (typeof(T) == typeof(Cinema)) DeleteSessions<T>($"CinemaId = {modelObject.Id}");
            else if (typeof(T) == typeof(Film)) DeleteSessions<T>($"FilmId = {modelObject.Id}");
        }

        public static void DeleteSessions<T>(string condition) where T : ISqlConverter
        {
            string sqlString = $"DELETE FROM {ModelIdentifier.GetTableName<T>()} " +
                $"WHERE {condition}";

            ExecuteNonQuery(sqlString);
        }


        private static void ExecuteNonQuery(string sqlString)
        {
            Connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
            sqlCommand.ExecuteNonQuery();
            Connection.Close();
        }
    }
}
