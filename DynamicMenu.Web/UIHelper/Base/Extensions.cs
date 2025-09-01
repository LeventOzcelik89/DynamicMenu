using System.Text.RegularExpressions;

namespace DynamicMenu.Web.UIHelper
{
    public static class Extensions
    {

        public static string AppendAttributes(this string item, IDictionary<string, object> attributes)
        {

            if (attributes == null)
                return item;

            foreach (var attr in attributes)
            {
                item = item.AppendAttribute(attr.Key.ToString().ToLower(), attr.Value);
            }
            ;

            return item;

        }

        public static string AppendAttribute(this string item, string Key, object Value)
        {

            var pat = Key + "=\"(.*?)\"";

            if (Regex.Match(item, pat).Success)
            {
                item = Regex.Replace(item, "" + Key + "=\"(.*?)\"", Key += "=\"" + Value + "\"", RegexOptions.IgnoreCase);
            }
            else
            {
                if (item.StartsWith("<input"))
                {
                    item = item.Insert(6, " " + Key + "=\"" + Value + "\"");
                }
                else if (item.StartsWith("<div"))
                {
                    item = item.Insert(4, " " + Key + "=\"" + Value + "\"");
                }
                else if (item.StartsWith("<textarea"))
                {
                    item = item.Insert(9, " " + Key + "=\"" + Value + "\"");
                }
                else if (item.StartsWith("<select"))
                {
                    item = item.Insert(7, " " + Key + "=\"" + Value + "\"");
                }

            }

            return item;

        }

    }
}
