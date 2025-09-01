using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Collections;

namespace DynamicMenu.Web.Helper
{
    public class JavaScriptInitializer : IJavaScriptInitializer
    {
        public virtual string Initialize(string id, string name, IDictionary<string, object> options)
        {
            return InitializeFor(RegexExtensions.EscapeRegex.Replace(id, "\\\\$1"), name, options);
        }

        public virtual string InitializeFor(string selector, string name, IDictionary<string, object> options)
        {
            return new StringBuilder().Append("kendo.syncReady(function(){jQuery(\"").Append(selector).Append("\").kendo")
                .Append(name)
                .Append("(")
                .Append(Serialize(options))
                .Append(");});")
                .ToString();
        }

        public virtual string InitializeFor(string selector, string name, IDictionary<string, object> options, bool isChildComponent)
        {
            if (!isChildComponent)
            {
                return InitializeFor(selector, name, options);
            }

            return new StringBuilder().Append("jQuery(`").Append(selector).Append("`).kendo")
                .Append(name)
                .Append("(")
                .Append(Serialize(options, '`'))
                .Append(");")
                .ToString();
        }

        public virtual IJavaScriptSerializer CreateSerializer()
        {
            return new DefaultJavaScriptSerializer();
        }

        public virtual string Serialize(IDictionary<string, object> @object, char quotes = '"')
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            foreach (KeyValuePair<string, object> item in @object)
            {
                object obj = item.Value;
                string text = obj as string;
                if (text != null)
                {
                    if (item.Key == "dataSourceId")
                    {
                        stringBuilder.Append(",\"dataSource\":");
                        stringBuilder.Append(text.JavaScriptStringEncode(addDoubleQuotes: false));
                        continue;
                    }

                    if (item.Key == "dependenciesId")
                    {
                        stringBuilder.Append(",\"dependencies\":");
                        stringBuilder.Append(text.JavaScriptStringEncode(addDoubleQuotes: false));
                        continue;
                    }
                }

                stringBuilder.Append(",").Append("\"" + item.Key + "\"").Append(":");
                if (text != null)
                {
                    if (item.Key == "url")
                    {
                        if (quotes == '`')
                        {
                            text = text.Replace("%7B", "{").Replace("%7D", "}");
                        }

                        stringBuilder.Append(quotes + text + quotes);
                    }
                    else
                    {
                        stringBuilder.Append(text.JavaScriptStringEncode(addDoubleQuotes: true, quotes));
                    }

                    continue;
                }

                if (obj is IDictionary<string, object> object2)
                {
                    stringBuilder.Append(Serialize(object2, quotes));
                    continue;
                }

                if (obj is IEnumerable<DateTime> dates)
                {
                    AppendDates(stringBuilder, dates);
                    continue;
                }

                if (obj is IEnumerable<IDictionary<string, object>> array)
                {
                    AppendArrayOfObjects(stringBuilder, array);
                    continue;
                }

                IJavaScriptSerializer javaScriptSerializer = CreateSerializer();
                if (obj is IEnumerable value)
                {
                    stringBuilder.Append(javaScriptSerializer.Serialize(value));
                }
                else if (obj is bool)
                {
                    AppendBoolean(stringBuilder, (bool)obj);
                }
                else if (obj is DateTime)
                {
                    AppendDate(stringBuilder, (DateTime)obj);
                }
                else if (obj is int)
                {
                    bool isRightToLeft = Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;
                    int num = (int)obj;
                    if (isRightToLeft && num < 0)
                    {
                        obj = "-" + Math.Abs(num);
                    }

                    stringBuilder.Append(obj);
                }
                else if (obj is double)
                {
                    stringBuilder.Append(((double)obj).ToString("r", CultureInfo.InvariantCulture));
                }
                else if (obj is float)
                {
                    stringBuilder.Append(((float)obj).ToString("r", CultureInfo.InvariantCulture));
                }
                else if (obj is Guid)
                {
                    stringBuilder.AppendFormat("\"{0}\"", obj.ToString());
                }
                else if (obj == null)
                {
                    stringBuilder.Append("null");
                }
                else if (obj.GetType().GetTypeInfo().IsPrimitive || obj is decimal)
                {
                    AppendConvertible(stringBuilder, obj);
                }
                else if (obj is ClientHandlerDescriptor value2)
                {
                    AppendEvent(stringBuilder, value2);
                }
                else if (obj is Enum)
                {
                    stringBuilder.Append(obj.ToString().ToLower().JavaScriptStringEncode(addDoubleQuotes: true));
                }
                else
                {
                    stringBuilder.Append(javaScriptSerializer.Serialize(obj));
                }
            }

            if (stringBuilder.Length >= 2)
            {
                stringBuilder.Remove(1, 1);
            }

            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        private void AppendBoolean(StringBuilder output, bool value)
        {
            if (value)
            {
                output.Append("true");
            }
            else
            {
                output.Append("false");
            }
        }

        private void AppendEvent(StringBuilder output, ClientHandlerDescriptor value)
        {
            if (value.HandlerName.HasValue())
            {
                output.Append(value.HandlerName);
            }
            else if (value.TemplateDelegate != null)
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    stringWriter.WriteContent(value.TemplateDelegate, HtmlEncoder.Default, value);
                    output.Append(stringWriter.ToString());
                }
            }
        }

        private void AppendDates(StringBuilder output, IEnumerable<DateTime> dates)
        {
            output.Append("[");
            if (dates.Any())
            {
                foreach (DateTime date in dates)
                {
                    AppendDate(output, date);
                    output.Append(",");
                }

                output.Remove(output.Length - 1, 1);
            }

            output.Append("]");
        }

        private void AppendArrayOfObjects(StringBuilder output, IEnumerable<IDictionary<string, object>> array)
        {
            output.Append("[");
            if (array.Any())
            {
                foreach (IDictionary<string, object> item in array)
                {
                    output.Append((item != null) ? Serialize(item) : "null");
                    output.Append(",");
                }

                output.Remove(output.Length - 1, 1);
            }

            output.Append("]");
        }

        private void AppendDate(StringBuilder output, DateTime value)
        {
            output.Append("new Date(").Append(value.Year).Append(",")
                .Append(value.Month - 1)
                .Append(",")
                .Append(value.Day)
                .Append(",")
                .Append(value.Hour)
                .Append(",")
                .Append(value.Minute)
                .Append(",")
                .Append(value.Second)
                .Append(",")
                .Append(value.Millisecond)
                .Append(")");
        }

        private void AppendConvertible(StringBuilder output, object value)
        {
            output.Append(Convert.ToString(value, CultureInfo.InvariantCulture));
        }
    }
}
