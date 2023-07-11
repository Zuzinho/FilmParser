using FilmParser.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilmParser.Database
{
    internal class DataBaseReader: DataBase
    {
        public async static Task<T> SelectAsync<T>(int id) where T : ISqlConverter
        {
            string sqlString = $"SELECT * FROM {TableNamesPairs[typeof(T)]} " +
                $"WHERE Id = {id}";

            using (var reader = await GetReaderAsync(sqlString))
            {
                return await reader.ReadAsync() ? (T)ModelFactory.GetModelObject<T>(reader) : default;
            }
        }

        public async static Task<List<T>> SelectAsync<T>(string conditions = "") where T : ISqlConverter
        {
            if (!conditions.Equals("")) conditions = "WHERE " + conditions;
            string sqlString = $"SELECT * FROM {TableNamesPairs[typeof(T)]} {conditions}";
            List<T> modelObjects = new List<T>();

            using (var reader = await GetReaderAsync(sqlString))
            {
                while (reader.Read()) modelObjects.Add((T)ModelFactory.GetModelObject<T>(reader));
            }
            return modelObjects;
        }

        public async static Task<string> GetSiteLinkAsync(string siteName)
        {
            string sqlString = $"SELECT Link FROM Sites WHERE Name = N'{siteName}'";
            return await GetStringValueAsync(sqlString);
        }

        public async static Task<int> GetSiteIdAsync(string siteName)
        {
            string sqlString = $"SELECT Id FROM Sites WHERE Name = N'{siteName}'";
            return await GetIntValueAsync(sqlString);
        }

        public async static Task<string> GetCinemaUIdAsync(int cinemaId, int siteId)
        {
            string sqlString = "SELECT CinemaUId FROM CinemasUId WHERE " +
                $"CinemaId = {cinemaId} AND SiteId = {siteId}";

            return await GetStringValueAsync(sqlString);
        }

        public async static Task<string> GetFilmUIdAsync(int filmId, int siteId)
        {
            string sqlString = "SELECT FilmUId FROM FilmsUId WHERE " +
                $"FilmId = {filmId} AND SiteId = {siteId}";

            return await GetStringValueAsync(sqlString);
        }
    }
}
