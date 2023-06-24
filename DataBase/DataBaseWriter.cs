using FilmParser.Model;
using System.Data.SqlClient;

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
                $"WHERE {ModelIdentifier.GetIdName<T>()}";

            ExecuteNonQuery(sqlString);
        }


        private static void ExecuteNonQuery(string sqlString)
        {
            using (Connection) {
                Connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
