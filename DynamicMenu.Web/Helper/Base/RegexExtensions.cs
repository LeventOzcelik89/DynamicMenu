using System.Text.RegularExpressions;

namespace DynamicMenu.Web.Helper
{
    internal static class RegexExtensions
    {
        //
        // Summary:
        //     Escape meta characters: http://api.jquery.com/category/selectors/
        internal static readonly Regex EscapeRegex = new Regex("([;&,\\.\\+\\*~'\\:\\\"\\!\\^\\$\\[\\]\\(\\)\\|\\/])", RegexOptions.Compiled, Regex.InfiniteMatchTimeout);

        //
        // Summary:
        //     Popup slashes Regex
        internal static readonly Regex PopupSlashes = new Regex("(?<=data-val-regex-pattern=\")([^\"]*)", RegexOptions.Multiline, Regex.InfiniteMatchTimeout);

        //
        // Summary:
        //     Unicode entity Regex
        internal static readonly Regex UnicodeEntityExpression = new Regex("\\\\+u(\\d+)\\\\*#(\\d+;)", RegexOptions.Compiled, Regex.InfiniteMatchTimeout);

        //
        // Summary:
        //     Name Regex
        internal static readonly Regex NameExpression = new Regex("([A-Z]+(?=$|[A-Z][a-z])|[A-Z]?[a-z]+)", RegexOptions.Compiled, Regex.InfiniteMatchTimeout);

        //
        // Summary:
        //     Entity Regex
        internal static readonly Regex EntityExpression = new Regex("(&amp;|&)#([0-9]+;)", RegexOptions.Compiled, Regex.InfiniteMatchTimeout);

        //
        // Summary:
        //     String Format Regex
        internal static readonly Regex StringFormatExpression = new Regex("(?<=\\{\\d:)(.)*(?=\\})", RegexOptions.Compiled, Regex.InfiniteMatchTimeout);

        //
        // Summary:
        //     Size Value Regex
        internal static readonly Regex SizeValueRegex = new Regex("^\\d+(px|%)$", RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);

        internal static string EncodeUrl(this string value)
        {
            value = Regex.Replace(value, "(%20)*%23%3D(%20)*", "#=", RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            value = Regex.Replace(value, "(%20)*%23(%20)*", "#", RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            value = Regex.Replace(value, "(%20)*%24%7B(%20)*", "${", RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            value = Regex.Replace(value, "(%20)*%7D(%20)*", "}", RegexOptions.IgnoreCase, Regex.InfiniteMatchTimeout);
            return value;
        }
    }
}
