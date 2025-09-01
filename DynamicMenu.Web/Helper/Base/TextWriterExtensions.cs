using Microsoft.AspNetCore.Mvc.Razor;
using System.Text.Encodings.Web;

namespace DynamicMenu.Web.Helper
{
    public static class TextWriterExtensions
    {
        public static void WriteContent<T>(this TextWriter writer, Func<T, object> action, HtmlEncoder htmlEncoder, T dataItem = null, bool htmlEncode = false) where T : class
        {
            object obj = action(dataItem);
            if (obj is HelperResult helperResult)
            {
                helperResult.WriteTo(writer, htmlEncoder);
            }
            else if (obj != null)
            {
                if (htmlEncode)
                {
                    writer.Write(htmlEncoder.Encode(obj.ToString()));
                }
                else
                {
                    writer.Write(obj.ToString());
                }
            }
        }
    }
}
