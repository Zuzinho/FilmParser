using FilmParser.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FilmParser.Parser.Interface
{
    internal interface IParser
    {
        IParserSettings ParserSettings { get; }
        HtmlLoader HtmlLoader { get; }

        Task<ICollection<int>> ParseCinemasAsync();
        Task<ICollection<int>> ParseFilmsAsync(int cinemaId);
        Task<ICollection<int>> ParseSessionsAsync(int cinemaId, int filmId);
    }
}
