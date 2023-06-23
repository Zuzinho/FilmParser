using System;

namespace FilmParser.Model
{
    internal class Session: ISqlConverter
    {
        public int Id { get; private set; }
        public int CinemaId { get; private set; }
        public int FilmId { get; private set; }
        public DateTime StartTime { get; private set; }
        public int Price { get; private set; }

        public Session(int id, int cinemaId, int filmId, DateTime startTime, int price)
        {
            Id = id;
            CinemaId = cinemaId;
            FilmId = filmId;
            StartTime = startTime;
            Price = price;
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
        }

        public string GetValuesString()
        {
            throw new NotImplementedException();
        }

        public string GetSetString()
        {
            throw new NotImplementedException();
        }

        public string GetIdCondition()
        {
            throw new NotImplementedException();
        }
    }
}
