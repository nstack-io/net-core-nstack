using System;

namespace NStack.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToLower(this string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("String must be at least of length 1", nameof(str));

            return $"{char.ToLower(str[0])}{str.Substring(1)}";
        }
    }
}
