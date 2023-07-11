using FilmParser.Parser.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using FilmParser.Database;
using System.Text.Json;
using AngleSharp.Dom;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using System.Globalization;

namespace FilmParser.Parser.KinoAfisha
{
    internal class KinoAfishaParser : IParser
    {
        public IParserSettings ParserSettings => new KinoAfishaParserSettings();

        public HtmlLoader HtmlLoader => new HtmlLoader(ParserSettings);

        public async Task<ICollection<int>> ParseCinemasAsync()
        {
            IHtmlDocument cinemasPage = await HtmlLoader.GetCinemasSourceAsync();
            if (cinemasPage == null) return new List<int>();
            HashSet<int> cinemaIds = new HashSet<int>();

            int count = 0;
            var dataParams = cinemasPage.GetElementsByClassName("cinemaList_item").Attr("data-param");
            foreach(var dataParam in dataParams)
            {
                string uidCinema = JsonSerializer.Deserialize<DataParamCinema>(dataParam).uid;

                IHtmlDocument cinemaPage = await HtmlLoader.GetCinemaSourceAsync(uidCinema);
                string name = cinemaPage.GetElementsByTagName("h1")[0].TextContent.Trim();
                string address = cinemaPage.GetElementsByClassName("theaterInfo_dataAddr")[0].TextContent.Trim();

                int cinemaId = await DataBaseWriter.InsertCinemaAsync(name, address);
                if (cinemaId == -1) continue;
                await DataBaseWriter.InsertCinemaUIdAsync(cinemaId, ParserSettings.SiteId, uidCinema);
                if (cinemaIds.Add(cinemaId)) count++;
                if (count == 10) break;
            }

            return cinemaIds;
        }

        public async Task<ICollection<int>> ParseFilmsAsync(int cinemaId)
        {
            string uidCinema = await DataBaseReader.GetCinemaUIdAsync(cinemaId, ParserSettings.SiteId);
            IHtmlDocument filmsPage = await HtmlLoader.GetFilmsSourceAsync(uidCinema);
            if (filmsPage == null) return new List<int>();
            HashSet<int> filmIds = new HashSet<int>();

            var dataParams = filmsPage.GetElementsByClassName("showtimes_fav").Attr("data-param");
            foreach(var dataParam in dataParams)
            {
                string uidFilm = JsonSerializer.Deserialize<DataParamFilm>(dataParam).uid.ToString();

                IHtmlDocument filmPage = await HtmlLoader.GetFilmSourceAsync(uidFilm);
                string name = filmPage.GetElementsByClassName("picture_image")[0].GetAttribute("title").Trim();
                string genre = filmPage.GetElementsByClassName("filmInfo_genreItem")[0].TextContent.Trim();
                string description = filmPage.GetElementsByClassName("tabs_contentItem js-active")[0].
                    GetElementsByTagName("p")[0].TextContent;

                int filmId = await DataBaseWriter.InsertFilmAsync(name, genre, description);
                if (filmId == -1) continue;
                await DataBaseWriter.InsertFilmUIdAsync(filmId, ParserSettings.SiteId, uidFilm);
                filmIds.Add(filmId);
            }

            return filmIds;
        }

        public async Task<ICollection<int>> ParseSessionsAsync(int cinemaId, int filmId)
        {
            string uidCinema = await DataBaseReader.GetCinemaUIdAsync(cinemaId, ParserSettings.SiteId);
            string uidFilm = await DataBaseReader.GetFilmUIdAsync(filmId, ParserSettings.SiteId);
            IHtmlDocument sessionsPage = await HtmlLoader.GetSessionsSourceAsync(uidCinema, uidFilm);
            if (sessionsPage == null) return new List<int>();
            HashSet<int> sessionIds = new HashSet<int>();

            var sessions = sessionsPage.GetElementsByClassName("showtimes_session").Where(
                element =>
                {
                    var dataParam = element.GetAttribute("data-param");
                    if (dataParam == null) return false;
                    return dataParam.Contains($"\"cinemaid\":\"{uidCinema}\",\"movieid\":\"{uidFilm}\"");
                });

            foreach (var session in sessions)
            {
                string time = session.GetElementsByClassName("session_time")[0].TextContent;
                DateTime startTime = DateTime.Today.Add(TimeSpan.ParseExact(time, "hh:mm", null));
                string priceString = session.GetElementsByClassName("session_price")[0].TextContent;
                Regex regex = new Regex("\\d+");
                string priceValue = regex.Match(priceString).Value;
                int price = Convert.ToInt32(priceValue);

                int sessionId = await DataBaseWriter.InsertSessionAsync(cinemaId, filmId, startTime, price);
                sessionIds.Add(sessionId);
            }

            return sessionIds;
        }

        private class DataParamCinema
        {
            public DataParamCinema(string type, string uid)
            {
                this.type = type;
                this.uid = uid;
            }
            public string type { get; }
            public string uid { get; }
        }

        private class DataParamFilm
        {
            public DataParamFilm(int uid, string type)
            {
                this.uid = uid;
                this.type = type;
            }
            public int uid { get; }
            public string type { get; }
        }
    }
}
