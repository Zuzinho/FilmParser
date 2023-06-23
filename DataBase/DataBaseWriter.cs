using FilmParser.Model;
using System.Data.SqlClient;

namespace FilmParser.DataBase
{
    internal class DataBaseWriter : DataBase
    {
        public static void InsertData(ISqlConverter modelObject)
        {
            string sqlString = $"INSERT INTO {modelObject.GetTableName()} {modelObject.GetValuesString()}";

            ExecuteNonQuery(sqlString);
        }

        public static void UpdateData(ISqlConverter modelObject)
        {
            string sqlString = $"UPDATE {modelObject.GetTableName()} {modelObject.GetSetString()} WHERE {modelObject.GetIdCondition()}";

            ExecuteNonQuery(sqlString);
        }


        private static void ExecuteNonQuery(string sqlString)
        {
            using (Connection)
            {
                Connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlString, Connection);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
