using FilmParser.Model;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FilmParser.DataBase
{
    internal class DataBaseWriter : DataBase
    {
        public async static Task InsertDataAsync<T>(T modelObject) where T : ISqlConverter
        {
            string sqlString = $"INSERT INTO {ModelIdentifier.GetTableName<T>()} {modelObject.GetValuesString()}";

            await ExecuteNonQueryAsync(sqlString);
        }

        public async static Task UpdateDataAsync<T>(T modelObject) where T : ISqlConverter
        {
            string sqlString = $"UPDATE {ModelIdentifier.GetTableName<T>()} {modelObject.GetSetString()} " +
                $"WHERE Id = {modelObject.Id}";

            await ExecuteNonQueryAsync(sqlString);
        }

        public async static void DeleteDataAsync<T>(T modelObject) where T : ISqlConverter
        {
            string sqlString = $"DELETE FROM {ModelIdentifier.GetTableName<T>()} " +
                $"WHERE Id = {modelObject.Id}";

            if (typeof(T) == typeof(Cinema)) DeleteSessions($"CinemaId = {modelObject.Id}");
            else if (typeof(T) == typeof(Film)) DeleteSessions($"FilmId = {modelObject.Id}");

            await ExecuteNonQueryAsync(sqlString);
        }

        public async static void DeleteSessionsAsync(string condition)
        {
            string sqlString = $"DELETE FROM Sessions WHERE {condition}";

            await ExecuteNonQueryAsync(sqlString);
        }


        private async static Task ExecuteNonQueryAsync(string sqlString)
        {
            Connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
            await sqlCommand.ExecuteNonQueryAsync();
            Connection.Close();
        }
    }
}
