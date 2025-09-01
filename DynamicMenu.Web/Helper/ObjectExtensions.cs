using System.Reflection;

namespace DynamicMenu.Web.Helper
{
    internal static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object @object)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            if (@object != null)
            {
                PropertyInfo[] properties = @object.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    dictionary.Add(propertyInfo.Name.Replace("_", "-"), propertyInfo.GetValue(@object));
                }
            }

            return dictionary;
        }

        public static IDictionary<string, string> ToStringDictionary(this object @object)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
            if (@object != null)
            {
                PropertyInfo[] properties = @object.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in properties)
                {
                    dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(@object).ToString());
                }
            }

            return dictionary;
        }

        public static T CopyValues<T>(this T target, T source, string[] skip)
        {
            foreach (PropertyInfo item in from prop in typeof(T).GetProperties()
                                          where prop.CanRead && prop.CanWrite
                                          select prop)
            {
                if (!skip.Contains(item.Name))
                {
                    object value = item.GetValue(source, null);
                    if (value != null)
                    {
                        item.SetValue(target, value, null);
                    }
                }
            }

            return target;
        }
    }
}
