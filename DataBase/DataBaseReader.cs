using FilmParser.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FilmParser.DataBase
{
    internal class DataBaseReader: DataBase
    {
        public async static Task<T> SelectAsync<T>(int id) where T : ISqlConverter
        {
            string sqlString = $"SELECT * FROM {ModelIdentifier.GetTableName<T>()} " +
                $"WHERE Id = {id}";
            T modelObject = default;

            var reader = await GetReaderAsync(sqlString);

            if(reader.Read()) modelObject = (T)(ModelFactory.GetModelObject<T>(reader));

            Connection.Close();

            return modelObject;
        }

        public async static Task<List<T>> SelectAsync<T>(string conditions = "") where T : ISqlConverter
        {
            if (!conditions.Equals("")) conditions = "WHERE " + conditions;
            string sqlString = $"SELECT * FROM {ModelIdentifier.GetTableName<T>()} {conditions}";
            List<T> modelObjects = new List<T>();

            var reader = await GetReaderAsync(sqlString);

            while (reader.Read()) modelObjects.Add((T)(ModelFactory.GetModelObject<T>(reader)));

            Connection.Close();

            return modelObjects;
        }


        private async static Task<SqlDataReader> GetReaderAsync(string sqlString)
        {
            Connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
            return await sqlCommand.ExecuteReaderAsync();
        }
    }
}
