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
                parser.ParseCinemas(),
                async cinema =>
                {
                    await DataBaseWriter.InsertDataAsync(cinema);
                    List<Film> films = await parser.ParseFilms(cinema.Id);
                    Parallel.ForEach(
                        films,
                        async film =>
                        {
                            await DataBaseWriter.InsertDataAsync(film);
                            List<Session> sessions = await parser.ParseSessions(cinema.Id, film.Id);
                            Parallel.ForEach(
                                sessions,
                                async session =>
                                {
                                    await DataBaseWriter.InsertDataAsync(session);
                                }
                            );
                        }
                    );
                }
            );
        }
    }
}
