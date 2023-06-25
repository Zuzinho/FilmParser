namespace FilmParser.Model
{
    internal interface ISqlConverter
    {
        int Id { get; }
        string GetValuesString();
        string GetSetString();
    }
}
