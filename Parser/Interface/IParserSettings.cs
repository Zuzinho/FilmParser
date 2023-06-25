namespace FilmParser.Parser.Interface
{
    internal interface IParserSettings
    {
        string Url { get; set; }
        string CinemaPrefix { get; set; }
        string FilmPrefix { get; set; }
    }
}
