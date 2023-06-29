namespace FilmParser.Parser.Interface
{
    internal interface IParserSettings
    {
        string BaseUrl { get; }
        string CinemaUrlPattern { get; }
        string FilmUrlPattern { get; }
        string CinemasUrlPattern { get; }
        string FilmsUrlPattern { get; }
        string SessionsUrlPattern { get; }
    }
}
