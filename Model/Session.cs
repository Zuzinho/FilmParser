using System;
using System.Data.SqlClient;

namespace FilmParser.Model
{
    internal class Session: ISqlConverter
    {
        private readonly int _id;

        public int Id => _id;
        public int CinemaId { get; private set; }
        public int FilmId { get; private set; }
        public DateTime StartTime { get; private set; }
        public int Price { get; private set; }
        private string StartTimeString
        {
            get
            {
                return StartTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public Session(int sessionId, int cinemaId, int filmId, DateTime startTime, int price)
        {
            _id = sessionId;
            CinemaId = cinemaId;
            FilmId = filmId;
            StartTime = startTime;
            Price = price;
        }

        public Session(SqlDataReader reader)
        {
            _id = (int)(reader.GetValue(0));
            CinemaId = (int)(reader.GetValue(1));
            FilmId = (int)(reader.GetValue(2));
            StartTime = (DateTime)(reader.GetValue(3));
            Price = (int)(reader.GetValue(4));
        }

        public string GetValuesString()
        {
            return "(CinemaId, FilmId, StartTime, Price) VALUES " +
                $"({CinemaId}, {FilmId}, '{StartTimeString}', {Price})";
        }

        public string GetSetString()
        {
            return $"SET CinemaId = {CinemaId}, FilmId = {FilmId}, StartTime = '{StartTimeString}', Price = {Price}";
        }
    }
}
