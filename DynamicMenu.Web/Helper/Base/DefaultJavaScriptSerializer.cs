using System.Text.Json;

namespace DynamicMenu.Web.Helper
{
    public class DefaultJavaScriptSerializer : IJavaScriptSerializer
    {
        public string Serialize(object value)
        {
            return JsonSerializer.Serialize(value).Replace("<", "\\u003c").Replace(">", "\\u003e");
        }
    }
}
