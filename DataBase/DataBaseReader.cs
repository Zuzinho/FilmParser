using FilmParser.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FilmParser.DataBase
{
    internal class DataBaseReader: DataBase
    {
        public static T Select<T>(int id) where T : ISqlConverter
        {
            string sqlString = $"SELECT * FROM {ModelIdentifier.GetTableName<T>()} " +
                $"WHERE Id = {id}";
            T modelObject = default;

            var reader = GetReader(sqlString);

            if(reader.Read()) modelObject = (T)(ModelFactory.GetModelObject<T>(reader));

            Connection.Close();

            return modelObject;
        }

        public static List<T> Select<T>(string conditions = "") where T : ISqlConverter
        {
            if (!conditions.Equals("")) conditions = "WHERE " + conditions;
            string sqlString = $"SELECT * FROM {ModelIdentifier.GetTableName<T>()} {conditions}";
            List<T> modelObjects = new List<T>();

            var reader = GetReader(sqlString);

            while (reader.Read()) modelObjects.Add((T)(ModelFactory.GetModelObject<T>(reader)));

            Connection.Close();

            return modelObjects;
        }


        private static SqlDataReader GetReader(string sqlString)
        {
            Connection.Open();
            SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
            var reader = sqlCommand.ExecuteReader();
            return reader;
        }
    }
}
