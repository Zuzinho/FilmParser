using FilmParser.Database;
using FilmParser.Parser.Interface;

namespace FilmParser.Parser.KinoAfisha
{
    internal class KinoAfishaParserSettings : IParserSettings
    {
        public string SiteName => "KinoAfisha";
        public int SiteId => DataBaseReader.GetSiteIdAsync(SiteName).Result;
        public string BaseUrl => DataBaseReader.GetSiteLinkAsync(SiteName).Result;

        public string CinemasUrlPattern => "cinema/";

        public string FilmsUrlPattern => CinemaUrlPattern + "schedule/";

        public string SessionsUrlPattern => FilmsUrlPattern;

        public string CinemaUrlPattern => "cinema/{cinemaId}/";

        public string FilmUrlPattern => "movies/{filmId}/";
    }
}
