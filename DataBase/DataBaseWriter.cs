using FilmParser.Model;
using System.Data.SqlClient;

namespace FilmParser.DataBase
{
    internal class DataBaseWriter: DataBase
    {
        public void InsertData(ISqlConverter modelObject)
        {
            string sqlString = $"INSERT INTO {modelObject.GetTableName()} " +
                $"{modelObject.GetVariablesString()} VALUES {modelObject.GetValuesString()}";

            using (connection)
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlString, connection);
                sqlCommand.ExecuteNonQuery();
            }
            
        }
    }
}
