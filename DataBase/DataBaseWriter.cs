using FilmParser.Model;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FilmParser.DataBase
{
    internal class DataBaseWriter : DataBase
    {
        public async static Task<int> InsertCinemaAsync(string name, string address = "")
        {
            string tableName = TableNamesPairs[typeof(Cinema)];
            string sqlString = $"INSERT INTO {tableName} (Name, Address) VALUES " +
                $"(N'{name}', N'{address}')";

            return await InsertAsync(sqlString, tableName);
        }

        public async static Task<int> InsertFilmAsync(string name, string genre, string description, string avatarPath = "")
        {
            string tableName = TableNamesPairs[typeof(Film)];
            string sqlString = $"INSERT INTO {tableName} (Name, Genre, Description, AvatarPath) VALUES " +
                $"(N'{name}', N'{genre}', N'{description}', N'{avatarPath}')";

            return await InsertAsync(sqlString, tableName);
        }

        public async static Task<int> InsertSessionAsync(int cinemaId, int filmId, DateTime startTime, int price)
        {
            string tableName = TableNamesPairs[typeof(Session)];
            string sqlString = $"INSERT INTO {tableName} (CinemaId, FilmId, StartTime, Price) VALUES " +
                $"({cinemaId}, {filmId}, '{startTime:yyyy-MM-dd HH:mm:ss}', {price})";

            return await InsertAsync(sqlString, tableName);
        }

        public async static Task UpdateCinemaAsync(int cinemaId, string name, string address = "")
        {
            string tableName = TableNamesPairs[typeof(Cinema)];
            string sqlString = $"UPDATE {tableName} SET " +
                $"Name = N'{name}' ," +
                $"Address = N'{address}' " +
                $"WHERE Id = {cinemaId}";

            try
            {
                await ExecuteNonQueryAsync(sqlString);
            }
            catch {}
        }

        public async static Task DeleteTAsync<T>(int id) where T : ISqlConverter
        {
            string tableName = TableNamesPairs[typeof(T)];
            string sqlString = $"DELETE FROM {tableName} WHERE Id = {id}";

            try
            {
                await ExecuteNonQueryAsync(sqlString);
            }
            catch {}
        }

        private static async Task<int> InsertAsync(string sqlString, string tableName)
        {
            try
            {
                await ExecuteNonQueryAsync(sqlString);
                int newId = await DataBaseReader.GetNewIdAsync(tableName);
                await IncrementId(tableName, newId);
                return newId;
            }
            catch (DbException)
            {
                return -1;
            }
        }

        private static async Task IncrementId(string tableName, int lastId)
        {
            string sqlString = $"UPDATE {TableNamesPairs[typeof(int)]} SET NewId = {lastId + 1} WHERE TableName = '{tableName}'";

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