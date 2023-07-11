using FilmParser.Model;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Web;

namespace FilmParser.Database
{
    internal class DataBaseWriter : DataBase
    {
        public static void ResetDataBase()
        {
            string sqlDropString = "DROP TABLE IF EXISTS CinemasUId, FilmsUId, Sessions, Cinemas, Films";
            string sqlCreateCinemasString = "CREATE TABLE [dbo].[Cinemas] (\r\n" +
                "    [Id]      INT            IDENTITY (1, 1) NOT NULL,\r\n" +
                "    [Name]    NVARCHAR (50)  NOT NULL,\r\n" +
                "    [Address] NVARCHAR (MAX) NULL,\r\n" +
                "    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n" +
                ");";
            string sqlCreateFilmsString = "CREATE TABLE [dbo].[Films] (\r\n" +
                "    [Id]          INT            IDENTITY (1, 1) NOT NULL,\r\n" +
                "    [Name]        NVARCHAR (100) NOT NULL,\r\n" +
                "    [Genre]       NVARCHAR (50)  NOT NULL,\r\n" +
                "    [Description] NVARCHAR (MAX) NOT NULL,\r\n" +
                "    [AvatarPath]  NVARCHAR (100) NULL,\r\n" +
                "    PRIMARY KEY CLUSTERED ([Id] ASC)\r\n" +
                ");";
            string sqlCreateSessionsString = "CREATE TABLE [dbo].[Sessions] (\r\n" +
                "    [Id]        INT      IDENTITY (1, 1) NOT NULL,\r\n" +
                "    [CinemaId]  INT      NOT NULL,\r\n" +
                "    [FilmId]    INT      NOT NULL,\r\n" +
                "    [StartTime] DATETIME NOT NULL,\r\n" +
                "    [Price]     INT      NOT NULL,\r\n" +
                "    PRIMARY KEY CLUSTERED ([Id] ASC), \r\n" +
                "    CONSTRAINT [FK_Sessions_Cinemas] FOREIGN KEY ([CinemaId]) REFERENCES [Cinemas]([Id]) ON DELETE CASCADE, \r\n" +
                "    CONSTRAINT [FK_Sessions_Films] FOREIGN KEY ([FilmId]) REFERENCES [Films]([Id]) ON DELETE CASCADE\r\n" +
                ");";
            string sqlCreateCinemasUIdString = "CREATE TABLE [dbo].[CinemasUId] (\r\n" +
                "    [CinemaId]  INT           NOT NULL,\r\n" +
                "    [SiteId]    INT           NOT NULL,\r\n" +
                "    [CinemaUId] NVARCHAR (20) NOT NULL,\r\n" +
                "    CONSTRAINT [FK_CinemasUId_Sites] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Sites] ([Id]) ON DELETE CASCADE,\r\n" +
                "    CONSTRAINT [FK_CinemasUId_Cinemas] FOREIGN KEY ([CinemaId]) REFERENCES [dbo].[Cinemas] ([Id]) ON DELETE CASCADE\r\n " +
                ");";
            string sqlCreateFilmsUIdString = "CREATE TABLE [dbo].[FilmsUId] (\r\n" +
                "    [FilmId]  INT           NOT NULL,\r\n" +
                "    [SiteId]  INT           NOT NULL,\r\n" +
                "    [FilmUId] NVARCHAR (20) NOT NULL,\r\n" +
                "    CONSTRAINT [FK_FilmsUid_Sites] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Sites] ([Id]) ON DELETE CASCADE,\r\n" +
                "    CONSTRAINT [FK_FilmsUid_Films] FOREIGN KEY ([FilmId]) REFERENCES [dbo].[Films] ([Id]) ON DELETE CASCADE\r\n" +
                ");";
            ExecuteNonQuery(sqlDropString);
            ExecuteNonQuery(sqlCreateFilmsString);
            ExecuteNonQuery(sqlCreateCinemasString);
            ExecuteNonQuery(sqlCreateSessionsString);
            ExecuteNonQuery(sqlCreateCinemasUIdString);
            ExecuteNonQuery(sqlCreateFilmsUIdString);
        }

        public async static Task<int> InsertCinemaAsync(string name, string address = "")
        {
            string tableName = TableNamesPairs[typeof(Cinema)];
            string sqlString = CreateSqlString(tableName,
                new string[] { "Name", "Address"},
                new string[] {name, address});

            return await InsertAsync(sqlString);
        }

        public async static Task<int> InsertFilmAsync(string name, string genre, string description, string avatarPath = "")
        {
            string tableName = TableNamesPairs[typeof(Film)];
            string sqlString = CreateSqlString(tableName,
                new string[] {"Name", "Genre", "Description", "AvatarPath"},
                new string[] {name, genre, description, avatarPath});

            return await InsertAsync(sqlString);
        }

        public async static Task<int> InsertSessionAsync(int cinemaId, int filmId, DateTime startTime, int price)
        {
            string tableName = TableNamesPairs[typeof(Session)];
            string sqlString = $"INSERT INTO {tableName} (CinemaId, FilmId, StartTime, Price) VALUES " +
                $"({cinemaId}, {filmId}, '{startTime:yyyy-MM-dd HH:mm:ss}', {price}); SELECT SCOPE_IDENTITY()";

            return await InsertAsync(sqlString);
        }

        public async static Task InsertCinemaUIdAsync(int cinemaId, int siteId, string cinemaUId)
        {
            string sqlString = "DECLARE @InsertedId INT;\r\n" +
                $"SET @InsertedId = (SELECT CinemaUId FROM CinemasUId WHERE CinemaId = {cinemaId} AND SiteId = {siteId});\r\n\r\n" +
                "IF @InsertedId IS NULL\r\n" +
                "BEGIN\r\n" +
                $"INSERT INTO CinemasUId (CinemaId, SiteId, CinemaUId) VALUES ({cinemaId}, {siteId}, N'{cinemaUId}');\r\n" +
                "END\r\n";

            await ExecuteNonQueryAsync(sqlString);
        }

        public async static Task InsertFilmUIdAsync(int filmId, int siteId, string filmUId)
        {
            string sqlString = "DECLARE @InsertedId INT;\r\n" +
                 $"SET @InsertedId = (SELECT FilmUId FROM FilmsUId WHERE FilmId = {filmId} AND SiteId = {siteId});\r\n\r\n" +
                 "IF @InsertedId IS NULL\r\n" +
                 "BEGIN\r\n" +
                 $"INSERT INTO FilmsUId (FilmId, SiteId, FilmUId) VALUES ({filmId}, {siteId}, N'{filmUId}');\r\n" +
                 "END\r\n";

            await ExecuteNonQueryAsync(sqlString);
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

        private static async Task<int> InsertAsync(string sqlString)
        {
            try
            {
                return await GetIntValueAsync(sqlString);
            }
            catch (DbException)
            {
                return -1;
            }
        }

        private static string CreateSqlString(string tableName, string[] keys, string[] values)
        {
            if (keys.Length != values.Length) throw new ArgumentException();
            if (keys.Length + values.Length == 0) throw new ArgumentException();
            string keys_string = keys[0];
            string values_string = $"N'{values[0]}'";
            for (int i = 1; i < keys.Length; i++)
            {
                keys_string += ',' + keys[i];
                values_string += $",N'{values[i]}'";
            }
            return "DECLARE @InsertedId INT;\r\n" +
                $"SET @InsertedId = (SELECT Id FROM {tableName} WHERE Name = N'{values[0]}');\r\n\r\n" +
                "IF @InsertedId IS NULL\r\n" +
                "BEGIN\r\n" +
                $"INSERT INTO {tableName} ({keys_string}) VALUES ({values_string});\r\n" +
                "SELECT SCOPE_IDENTITY()\r\n" +
                "END\r\n" +
                "ELSE\r\n" +
                "BEGIN\r\n" +
                "SELECT @InsertedId\r\n" +
                "END";
        }
    }
}