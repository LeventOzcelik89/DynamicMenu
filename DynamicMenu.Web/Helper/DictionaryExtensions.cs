using System.Text;
using System.Text.Encodings.Web;

namespace DynamicMenu.Web.Helper
{
    public static class DictionaryExtensions
    {
        //
        // Summary:
        //     Merges the specified instance.
        //
        // Parameters:
        //   instance:
        //     The instance.
        //
        //   key:
        //     The key.
        //
        //   value:
        //     The value.
        //
        //   replaceExisting:
        //     if set to true [replace existing].
        public static void Merge(this IDictionary<string, object> instance, string key, object value, bool replaceExisting)
        {
            if (replaceExisting || !instance.ContainsKey(key))
            {
                instance[key] = value;
            }
        }

        //
        // Summary:
        //     Appends the in value.
        //
        // Parameters:
        //   instance:
        //     The instance.
        //
        //   key:
        //     The key.
        //
        //   separator:
        //     The separator.
        //
        //   value:
        //     The value.
        public static void AppendInValue(this IDictionary<string, object> instance, string key, string separator, object value)
        {
            instance[key] = (instance.ContainsKey(key) ? (instance[key]?.ToString() + separator + value) : value.ToString());
        }

        //
        // Summary:
        //     Appends the specified value at the beginning of the existing value
        //
        // Parameters:
        //   instance:
        //
        //   key:
        //
        //   separator:
        //
        //   value:
        public static void PrependInValue(this IDictionary<string, object> instance, string key, string separator, object value)
        {
            instance[key] = (instance.ContainsKey(key) ? (value?.ToString() + separator + instance[key]) : value.ToString());
        }

        //
        // Summary:
        //     Merges the specified instance.
        //
        // Parameters:
        //   instance:
        //     The instance.
        //
        //   from:
        //     From.
        //
        //   replaceExisting:
        //     if set to true [replace existing].
        public static void Merge(this IDictionary<string, object> instance, IDictionary<string, object> from, bool replaceExisting)
        {
            foreach (KeyValuePair<string, object> item in from)
            {
                if (replaceExisting || !instance.ContainsKey(item.Key) || instance[item.Key] == null)
                {
                    instance[item.Key] = item.Value;
                }
            }
        }

        //
        // Summary:
        //     Merges the specified instance.
        //
        // Parameters:
        //   instance:
        //     The instance.
        //
        //   from:
        //     From.
        public static void Merge(this IDictionary<string, object> instance, IDictionary<string, object> from)
        {
            instance.Merge(from, replaceExisting: true);
        }

        //
        // Summary:
        //     Merges the specified instance.
        //
        // Parameters:
        //   instance:
        //     The instance.
        //
        //   values:
        //     The values.
        //
        //   replaceExisting:
        //     if set to true [replace existing].
        public static void Merge(this IDictionary<string, object> instance, object values, bool replaceExisting)
        {
            instance.Merge(new RouteValueDictionary(values), replaceExisting);
        }

        //
        // Summary:
        //     Merges the specified instance.
        //
        // Parameters:
        //   instance:
        //     The instance.
        //
        //   values:
        //     The values.
        public static void Merge(this IDictionary<string, object> instance, object values)
        {
            instance.Merge(values, replaceExisting: true);
        }

        public static IDictionary<string, object> Add<T>(this IDictionary<string, object> instance, string key, T value, T defaultValue) where T : IComparable
        {
            if (value != null && value.CompareTo(defaultValue) != 0)
            {
                instance[key] = value;
            }

            return instance;
        }

        public static IDictionary<string, object> Add<T>(this IDictionary<string, object> instance, string key, T value, Func<bool> condition)
        {
            if (condition())
            {
                instance[key] = value;
            }

            return instance;
        }

        //
        // Summary:
        //     Toes the attribute string.
        //
        // Parameters:
        //   instance:
        //     The instance.
        public static string ToAttributeString(this IDictionary<string, object> instance)
        {
            StringBuilder stringBuilder = new StringBuilder();
            HtmlEncoder @default = HtmlEncoder.Default;
            foreach (KeyValuePair<string, object> item in instance)
            {
                stringBuilder.Append(" {0}=\"{1}\"".FormatWith(@default.Encode(item.Key), @default.Encode(item.Value.ToString())));
            }

            return stringBuilder.ToString();
        }
    }
}
