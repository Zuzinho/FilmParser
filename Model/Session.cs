using System;
using System.Data.SqlClient;

namespace FilmParser.Model
{
    internal class Session: ISqlConverter
    {
        public int SessionId { get; private set; }
        public int CinemaId { get; private set; }
        public int FilmId { get; private set; }
        public DateTime StartTime { get; private set; }
        public int Price { get; private set; }

        public Session(int sessionId, int cinemaId, int filmId, DateTime startTime, int price)
        {
            SessionId = sessionId;
            CinemaId = cinemaId;
            FilmId = filmId;
            StartTime = startTime;
            Price = price;
        }

        public Session(SqlDataReader reader)
        {
            SessionId = (int)(reader.GetValue(0));
            CinemaId = (int)(reader.GetValue(1));
            FilmId = (int)(reader.GetValue(2));
            StartTime = (DateTime)(reader.GetValue(3));
            Price = (int)(reader.GetValue(4));
        }

        public string GetValuesString()
        {
            return "(CinemaId, FilmId, StartTime, Price) VALUES " +
                $"({CinemaId}, {FilmId}, '{StartTime}', {Price})";
        }

        public string GetSetString()
        {
            return $"SET CinemaId = {CinemaId}, FilmId = {FilmId}, StartTime = '{StartTime}', Price = {Price}";
        }
    }
}
