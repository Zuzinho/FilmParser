using FilmParser.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmParser.Parser.Interface
{
    internal interface IParser
    {
        List<Cinema> ParseCinemas();
        Task<List<Film>> ParseFilms(int cinemaId);
        Task<List<Session>> ParseSessions(int cinemaId, int filmId);
    }
}
