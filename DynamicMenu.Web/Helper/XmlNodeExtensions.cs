using System.Globalization;
using System.Xml;

namespace DynamicMenu.Web.Helper
{
    public static class XmlNodeExtensions
    {
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     Child element with name specified by childName does not exists.
        public static string ChildElementInnerText(this XmlNode node, string childName)
        {
            XmlElement xmlElement = node[childName];
            if (xmlElement == null)
            {
                string.Format(CultureInfo.CurrentCulture, "Child element with specified name: {0} cannot be found.", childName);
                return null;
            }

            return xmlElement.InnerText;
        }
    }
}
