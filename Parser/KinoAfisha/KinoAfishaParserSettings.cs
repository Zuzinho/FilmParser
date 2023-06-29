using FilmParser.Parser.Interface;

namespace FilmParser.Parser.KinoAfisha
{
    internal class KinoAfishaParserSettings : IParserSettings
    {
        public string BaseUrl => "https://www.kinoafisha.info/";

        public string CinemasUrlPattern => "cinema/";

        public string FilmsUrlPattern => CinemaUrlPattern + "schedule/";

        public string SessionsUrlPattern => FilmsUrlPattern;

        public string CinemaUrlPattern => "cinema/{cinemaId}/";

        public string FilmUrlPattern => "movies/{filmId}/";
    }
}
