using FilmParser.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FilmParser.DataBase
{
    internal class DataBaseReader: DataBase
    {
        public static ISqlConverter Select<T>(int id) where T : ISqlConverter
        {
            string sqlString = $"SELECT * FROM {ModelIdentifier.GetTableName<T>()} " +
                $"WHERE {ModelIdentifier.GetIdName<T>()} = {id}";
            var reader = GetReader(sqlString);

            if(reader.Read()) return ModelFactory.GetModelObject<T>(reader);
            return null;
        }

        public static List<ISqlConverter> Select<T>(string conditions = "") where T : ISqlConverter
        {
            if (!conditions.Equals("")) conditions = "WHERE " + conditions;
            string sqlString = $"SELECT * FROM {ModelIdentifier.GetTableName<T>()} {conditions}";
            var reader = GetReader(sqlString);

            List<ISqlConverter> modelObject = new List<ISqlConverter>();

            while (reader.Read()) modelObject.Add(ModelFactory.GetModelObject<T>(reader));

            return modelObject;
        }


        private static SqlDataReader GetReader(string sqlString)
        {
            using (Connection) {
                Connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
                return sqlCommand.ExecuteReader();
            }
        }
    }
}
