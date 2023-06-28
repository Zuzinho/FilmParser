using FilmParser.Parser.Interface;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;

namespace FilmParser.Parser
{
    internal class HtmlLoader
    {
        private readonly IParserSettings _parserSettings;
        private readonly HttpClient _client;
        private readonly HtmlParser _htmlParser;

        public HtmlLoader(IParserSettings parserSettings)
        {
            _parserSettings = parserSettings;
            _client = new HttpClient();
            _htmlParser = new HtmlParser();

        }

        public async Task<IHtmlDocument> GetCinemasSourceAsync()
        {
            string url = $"{_parserSettings.BaseUrl}/{_parserSettings.CinemasPrefixPattern}";
            
            return await GetHtmlDocumentAsync(url);
        }

        public async Task<IHtmlDocument> GetFilmsSourceAsync(int cinemaId)
        {
            string url = $"{_parserSettings.BaseUrl}/{_parserSettings.CinemasPrefixPattern}/" +
                $"{_parserSettings.FilmsPrefixPattern.Replace("{cinemaId}", cinemaId.ToString())}";

            return await GetHtmlDocumentAsync(url);
        }

        public async Task<IHtmlDocument> GetSessionsSourceAsync(int cinemaId, int filmId)
        {
            string url = $"{_parserSettings.BaseUrl}/{_parserSettings.CinemasPrefixPattern}/" +
                $"{_parserSettings.FilmsPrefixPattern.Replace("{cinemaId}", cinemaId.ToString())}/" +
                $"{_parserSettings.SessionsPrefixPattern.Replace("{filmId}", filmId.ToString())}";

            return await GetHtmlDocumentAsync(url);
        }

        private async Task<IHtmlDocument> GetHtmlDocumentAsync(string url)
        {
            var response = await _client.GetAsync(url);

            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string source = await response.Content.ReadAsStringAsync();

                return await _htmlParser.ParseDocumentAsync(source);
            }

            return null;
        }
    }
}
