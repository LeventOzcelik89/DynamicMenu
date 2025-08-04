using System.Globalization;
using System.Text;

namespace DynamicMenu.Web.Helper
{
    public static class StringExtensions
    {
        //
        // Summary:
        //     Replaces the format item in a specified System.String with the text equivalent
        //     of the value of a corresponding System.Object instance in a specified array.
        //
        //
        // Parameters:
        //   instance:
        //     A string to format.
        //
        //   args:
        //     An System.Object array containing zero or more objects to format.
        //
        // Returns:
        //     A copy of format in which the format items have been replaced by the System.String
        //     equivalent of the corresponding instances of System.Object in args.
        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, instance, args);
        }

        public static string EscapeHtmlEntities(this string html)
        {
            return RegexExtensions.EntityExpression.Replace(html, "$1\\\\#$2");
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        //
        // Summary:
        //     Determines whether this instance and another specified System.String object have
        //     the same value.
        //
        // Parameters:
        //   instance:
        //     The string to check equality.
        //
        //   comparing:
        //     The comparing with string.
        //
        // Returns:
        //     true if the value of the comparing parameter is the same as this string; otherwise,
        //     false.
        public static bool IsCaseSensitiveEqual(this string instance, string comparing)
        {
            return string.CompareOrdinal(instance, comparing) == 0;
        }

        //
        // Summary:
        //     Determines whether this instance and another specified System.String object have
        //     the same value.
        //
        // Parameters:
        //   instance:
        //     The string to check equality.
        //
        //   comparing:
        //     The comparing with string.
        //
        // Returns:
        //     true if the value of the comparing parameter is the same as this string; otherwise,
        //     false.
        public static bool IsCaseInsensitiveEqual(this string instance, string comparing)
        {
            return string.Compare(instance, comparing, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public static string ToCamelCase(this string instance)
        {
            return instance[0].ToString().ToLowerInvariant() + instance.Substring(1);
        }

        public static string AsTitle(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            int num = value.LastIndexOf(".", StringComparison.Ordinal);
            if (num > -1)
            {
                value = value.Substring(num + 1);
            }

            return value.SplitPascalCase();
        }

        public static T ToEnum<T>(this string value, T defaultValue)
        {
            if (!value.HasValue())
            {
                return defaultValue;
            }

            try
            {
                return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
            }
            catch (ArgumentException)
            {
                return defaultValue;
            }
        }

        public static string SplitPascalCase(this string value)
        {
            return RegexExtensions.NameExpression.Replace(value, " $1").Trim();
        }

        public static string JavaScriptStringEncode(this string value, bool addDoubleQuotes, char quotes = '"')
        {
            string text = value.JavaScriptStringEncode();
            if (!addDoubleQuotes)
            {
                return text;
            }

            return quotes + text + quotes;
        }

        public static string JavaScriptStringEncode(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = null;
            int startIndex = 0;
            int num = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (CharRequiresJavaScriptEncoding(c))
                {
                    if (stringBuilder == null)
                    {
                        stringBuilder = new StringBuilder(value.Length + 5);
                    }

                    if (num > 0)
                    {
                        stringBuilder.Append(value, startIndex, num);
                    }

                    startIndex = i + 1;
                    num = 0;
                }

                switch (c)
                {
                    case '\b':
                        stringBuilder.Append("\\b");
                        continue;
                    case '\t':
                        stringBuilder.Append("\\t");
                        continue;
                    case '\n':
                        stringBuilder.Append("\\n");
                        continue;
                    case '\f':
                        stringBuilder.Append("\\f");
                        continue;
                    case '\r':
                        stringBuilder.Append("\\r");
                        continue;
                    case '"':
                        stringBuilder.Append("\\\"");
                        continue;
                    case '\\':
                        stringBuilder.Append("\\\\");
                        continue;
                }

                if (!CharRequiresJavaScriptEncoding(c))
                {
                    num++;
                }
                else
                {
                    AppendCharAsUnicodeJavaScript(stringBuilder, c);
                }
            }

            if (stringBuilder == null)
            {
                return value;
            }

            if (num > 0)
            {
                stringBuilder.Append(value, startIndex, num);
            }

            return stringBuilder.ToString();
        }

        public static string RemoveWhiteSpace(this string instance)
        {
            return new string(instance.Where((char c) => !char.IsWhiteSpace(c)).ToArray());
        }

        public static string PascalCaseToKebabCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return string.Concat(value.Select(delegate (char character, int index)
            {
                ReadOnlySpan<char> readOnlySpan = ((char.IsUpper(character) && index > 0) ? "-" : string.Empty);
                char reference = char.ToLower(character);
                return string.Concat(readOnlySpan, new ReadOnlySpan<char>(ref reference));
            }));
        }

        public static string KebabCaseToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.Contains(" "))
            {
                return value;
            }

            return string.Join(string.Empty, from word in value.Split('-')
                                             select word.First().ToString().ToUpper() + word.Substring(1).ToLower());
        }

        private static bool CharRequiresJavaScriptEncoding(char c)
        {
            if (c < ' ' || c == '"' || c == '\\' || c == '\'' || c == '<' || c == '>' || c == '&' || c == '\u0085' || c == '\u2028')
            {
                return true;
            }

            return c == '\u2029';
        }

        private static void AppendCharAsUnicodeJavaScript(StringBuilder builder, char c)
        {
            builder.Append("\\u");
            int num = c;
            builder.Append(num.ToString("x4", CultureInfo.InvariantCulture));
        }

        //
        // Summary:
        //     Constructs a style string attribute by extracting style values that match a valid
        //     condition.
        internal static string ToStyleString(params string[] styles)
        {
            IEnumerable<string> values = from x in styles
                                         where !string.IsNullOrWhiteSpace(x)
                                         select x.TrimEnd(';');
            return string.Join(";", values);
        }

        //
        // Summary:
        //     Constructs a style string attribute by extracting style values that match a valid
        //     condition.
        internal static string ToStyleString(Dictionary<string, bool> stylesAndConditions)
        {
            IEnumerable<string> values = from x in stylesAndConditions
                                         where x.Value
                                         select x.Key;
            return string.Join(";", values);
        }

        //
        // Summary:
        //     Constructs a class string attribute by extracting class names that match a valid
        //     condition.
        internal static string ToClassString(Dictionary<string, bool> classesAndConditions)
        {
            IEnumerable<string> values = from x in classesAndConditions
                                         where x.Value
                                         select x.Key;
            return string.Join(" ", values);
        }

        //
        // Summary:
        //     Constructs a class string attribute by joining non-empty class names.
        internal static string ToClassString(params string[] classNames)
        {
            return string.Join(" ", classNames.Where((string className) => !string.IsNullOrEmpty(className)));
        }
    }
}
