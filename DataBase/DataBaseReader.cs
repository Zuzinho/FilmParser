using FilmParser.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FilmParser.DataBase
{
    internal class DataBaseReader: DataBase
    {
        public static Film SelectFilm(int filmId)
        {
            string sqlString = $"SELECT * FROM Films WHERE FilmId = {filmId}";
            var reader = GetReader(sqlString);

            if(reader.Read()) return new Film(reader);
            return null;
        }

        public static Cinema SelectCinema(int cinemaId)
        {
            string sqlString = $"SELECT * FROM Cinemas WHERE CinemaId = {cinemaId}";
            var reader = GetReader(sqlString);

            if (reader.Read()) return new Cinema(reader);
            return null;
        }

        public static List<Film> SelectFilms(string conditions = "FilmId > 0")
        {
            string sqlString = $"SELECT * FROM Films WHERE {conditions}";
            var reader = GetReader(sqlString);

            List<Film> films = new List<Film>();

            while(reader.Read()) films.Add(new Film(reader));

            return films;
        }

        public static List<Cinema> SelectCinemas(string conditions = "CinemaId > 0")
        {
            string sqlString = $"SELECT * FROM Cinemas WHERE {conditions}";
            var reader = GetReader(sqlString);

            List<Cinema> cinemas = new List<Cinema>();

            while (reader.Read()) cinemas.Add(new Cinema(reader));

            return cinemas;
        }

        public static List<Session> SelectSessions(string conditions = "SessionId > 0")
        {
            string sqlString = $"SELECT * FROM Sessions WHERE {conditions}";
            var reader = GetReader(sqlString);

            List<Session> sessions = new List<Session>();

            while (reader.Read()) sessions.Add(new Session(reader));

            return sessions;
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
