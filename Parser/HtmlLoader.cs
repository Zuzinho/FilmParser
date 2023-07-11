using FilmParser.Parser.Interface;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;
using System.Net;

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
            string url = $"{_parserSettings.BaseUrl}{_parserSettings.CinemasUrlPattern}";
            
            return await GetHtmlDocumentAsync(url);
        }

        public async Task<IHtmlDocument> GetFilmsSourceAsync(string cinemaId)
        {
            string url = $"{_parserSettings.BaseUrl}" +
                $"{_parserSettings.FilmsUrlPattern.Replace("{cinemaId}", cinemaId)}";

            return await GetHtmlDocumentAsync(url);
        }

        public async Task<IHtmlDocument> GetSessionsSourceAsync(string cinemaId, string filmId)
        {
            string url = $"{_parserSettings.BaseUrl}" +
                $"{_parserSettings.SessionsUrlPattern.Replace("{cinemaId}", cinemaId).Replace("{filmId}", filmId)}";

            return await GetHtmlDocumentAsync(url);
        }

        public async Task<IHtmlDocument> GetCinemaSourceAsync(string cinemaId)
        {
            string url = $"{_parserSettings.BaseUrl}" +
                $"{_parserSettings.CinemaUrlPattern.Replace("{cinemaId}", cinemaId)}";
        
            return await GetHtmlDocumentAsync(url);
        }

        public async Task<IHtmlDocument> GetFilmSourceAsync(string filmId)
        {
            string url = $"{_parserSettings.BaseUrl}" +
                $"{_parserSettings.FilmUrlPattern.Replace("{filmId}", filmId)}";

            return await GetHtmlDocumentAsync(url);
        }

        private async Task<IHtmlDocument> GetHtmlDocumentAsync(string url)
        {
            HttpResponseMessage response;
            try
            {
                response = await _client.GetAsync(url);
            }
            catch (HttpRequestException)
            {
                response = await _client.GetAsync(url);
            }
            catch (WebException)
            {
                return null;
            }

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                string source = await response.Content.ReadAsStringAsync();

                return _htmlParser.ParseDocumentAsync(source).Result;
            }

            return null;
        }
    }
}
