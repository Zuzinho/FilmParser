using System.Runtime.InteropServices;

namespace FilmParser.Model
{
    internal interface ISqlConverter
    {
        string GetTableName();
        string GetVariablesString();
        string GetValuesString();
    }
}
