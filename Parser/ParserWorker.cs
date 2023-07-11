using FilmParser.Database;
using FilmParser.Parser.Interface;
using System.Threading.Tasks;

namespace FilmParser.Parser
{
    internal class ParserWorker
    {
        public static void Work(IParser parser)
        {
            DataBase.OpenConnection();
            Parallel.ForEach(
                parser.ParseCinemasAsync().Result,
                cinemaId =>
                {
                    Parallel.ForEach(
                        parser.ParseFilmsAsync(cinemaId).Result,
                        filmId =>
                        {
                            _ = parser.ParseSessionsAsync(cinemaId, filmId).Result;
                        }
                    );
                }
            );
            DataBase.CloseConnection();
        }
    }
}
