using FilmParser.Model;
using FilmParser.Parser.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using FilmParser.DataBase;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AngleSharp.Dom;
using System.IO;

namespace FilmParser.Parser.KinoAfisha
{
    internal class KinoAfishaParser : IParser
    {
        public IParserSettings parserSettings => new KinoAfishaParserSettings();

        public HtmlLoader HtmlLoader => new HtmlLoader(parserSettings);

        private Dictionary<int, string> _cinemaIdPairs = new Dictionary<int, string>();
        private Dictionary<int, string> _filmIdPairs = new Dictionary<int, string>();

        public async Task<List<int>> ParseCinemas()
        {
            IHtmlDocument cinemasPage = HtmlLoader.GetCinemasSourceAsync().Result;

            var dataParams = cinemasPage.GetElementsByClassName("cinemaList_item").Attr("data-param");
            foreach(var dataParam in dataParams)
            {
                string uidCinema = JsonSerializer.Deserialize<DataParam>(dataParam).uid;

                IHtmlDocument cinemaPage = HtmlLoader.GetCinemaSourceAsync(uidCinema).Result;
                string name = cinemaPage.GetElementsByTagName("h1")[0].TextContent.Trim();
                string address = cinemaPage.GetElementsByClassName("theaterInfo_dataAddr")[0].TextContent.Trim();

                int cinemaId = await DataBaseWriter.InsertCinemaAsync(name, address);
                _cinemaIdPairs.Add(cinemaId, uidCinema);
            }

            return _cinemaIdPairs.Keys.ToList();
        }

        public async Task<List<int>> ParseFilms(int cinemaId)
        {
            string uidCinema = _cinemaIdPairs[cinemaId];
            IHtmlDocument filmsPage = HtmlLoader.GetFilmsSourceAsync(uidCinema).Result;

            var dataParams = filmsPage.GetElementsByClassName("showtimes_fav").Attr("data-param");
            foreach(var dataParam in dataParams)
            {
                string uidFilm = JsonSerializer.Deserialize<DataParam>(dataParam).uid;

                IHtmlDocument filmPage = HtmlLoader.GetFilmSourceAsync(uidFilm).Result;
                string name = filmPage.GetElementsByClassName("picture_image")[0].GetAttribute("title").Trim();
                string genre = filmPage.GetElementsByClassName("filmInfo_genreItem")[0].TextContent.Trim();
                string description = filmPage.GetElementsByClassName("tabs_contentItem js-active")[0].
                    GetElementsByTagName("p")[0].TextContent;

                int filmId = await DataBaseWriter.InsertFilmAsync(name, genre, description);
                _filmIdPairs.Add(filmId, uidFilm);
            }

            return _filmIdPairs.Keys.ToList();
        }

        public async Task<List<int>> ParseSessions(int cinemaId, int filmId)
        {

        }

        private class DataParam
        {
            internal string type;
            internal string uid;
        }
    }
}
