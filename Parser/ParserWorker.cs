using FilmParser.DataBase;
using FilmParser.Model;
using FilmParser.Parser.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmParser.Parser
{
    internal class ParserWorker
    {
        public void Work(IParser parser)
        {
            Parallel.ForEach(
                parser.ParseCinemasAsync(),
                cinemaId =>
                {
                    Parallel.ForEach(
                        parser.ParseFilms(cinemaId),
                        filmId =>
                        {
                            parser.ParseSessions(cinemaId, filmId);
                        }
                    );
                }
            );
        }
    }
}
