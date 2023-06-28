namespace FilmParser.Parser.Interface
{
    internal interface IParserSettings
    {
        string BaseUrl { get; }
        string CinemasPrefixPattern { get; }
        string FilmsPrefixPattern { get; }
        string SessionsPrefixPattern { get; }
    }
}
