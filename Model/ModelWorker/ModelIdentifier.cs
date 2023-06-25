﻿using System;

namespace FilmParser.Model
{
    internal static class ModelIdentifier
    {
        public static string GetTableName<T>() where T : ISqlConverter
        {
            Type type = typeof(T);
            if (type == typeof(Cinema)) return "Cinemas";
            if (type == typeof(Film)) return "Films";
            if (type == typeof(Session)) return "Sessions";

            return null;
        }
    }
}