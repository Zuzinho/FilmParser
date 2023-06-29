using FilmParser.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FilmParser.Parser.Interface
{
    internal interface IParser
    {
        IParserSettings parserSettings { get; }
        HtmlLoader HtmlLoader { get; }

        Task<List<int>> ParseCinemas();
        Task<List<int>> ParseFilms(int cinemaId);
        Task<List<int>> ParseSessions(int cinemaId, int filmId);
    }
}
