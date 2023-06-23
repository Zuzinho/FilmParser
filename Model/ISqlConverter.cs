namespace FilmParser.Model
{
    internal interface ISqlConverter
    {
        string GetTableName();
        string GetValuesString();
        string GetSetString();
        string GetIdCondition();
    }
}
