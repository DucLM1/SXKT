using System;
using System.Collections.Generic;
using System.Linq;

namespace SXKT.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string text)
        {
            if (!string.IsNullOrEmpty(text) && text.Length > 1)
            {
                return Char.ToLowerInvariant(text[0]) + text.Substring(1);
            }
            return text;
        }

        public static string ToStandardJsonKey(this string text)
        {
            return text.ToCamelCase();
        }

        public static List<int> ToListInt(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<int>();
            try
            {
                var ids = text.Split(",").Select(s => int.Parse(s)).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}