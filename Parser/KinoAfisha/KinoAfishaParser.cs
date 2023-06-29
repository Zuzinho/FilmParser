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

namespace FilmParser.Parser.KinoAfisha
{
    internal class KinoAfishaParser : IParser
    {
        public IParserSettings parserSettings => new KinoAfishaParserSettings();

        public HtmlLoader HtmlLoader => new HtmlLoader(parserSettings);

        private Dictionary<int, int> _cinemaIdPairs = new Dictionary<int, int>();
        private Dictionary<int, int> _filmIdPairs = new Dictionary<int, int>();

        public async Task<List<int>> ParseCinemas()
        {
            IHtmlDocument htmlDocument = HtmlLoader.GetCinemasSourceAsync().Result;
            List<int> cinemaIds = new List<int>();

            var cinemaListItems = htmlDocument.GetElementsByClassName("cinemaList_item");
            foreach(var cinemaListItem in cinemaListItems)
            {
                string name = cinemaListItem.GetElementsByClassName("cinemaList_name")[0].TextContent.Trim();
                string dataParamString = cinemaListItem.GetAttribute("data-param");
                string uid = JsonSerializer.Deserialize<DataParam>(dataParamString).uid;
                
                int cinemaId = await DataBaseWriter.InsertCinemaAsync(name);
                _cinemaIdPairs.Add(cinemaId, Convert.ToInt32(uid));
                cinemaIds.Add(cinemaId);
            }

            return cinemaIds;
        }

        public async Task<List<int>> ParseFilms(int cinemaId)
        {
            IHtmlDocument htmlDocument = HtmlLoader.GetFilmsSourceAsync(cinemaId).Result;
            List<int> filmIds = new List<int>();

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
