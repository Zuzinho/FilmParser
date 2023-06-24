namespace FilmParser.Model
{
    internal interface ISqlConverter
    {
        string GetValuesString();
        string GetSetString();
    }
}
