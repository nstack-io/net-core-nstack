using System;

namespace NStack.SDK.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the first character in the string to lower case.
        /// </summary>
        /// <param name="str">The string to perform the operation on.</param>
        public static string FirstCharToLower(this string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("String must be at least of length 1", nameof(str));

            return $"{char.ToLower(str[0])}{str.Substring(1)}";
        }

        /// <summary>
        /// Translates the NStack boolean string to a boolean.
        /// </summary>
        /// <param name="boolString">The string to get the boolean value from.</param>
        public static bool TranslateToBool(this string boolString) => boolString == "1";
    }
}
