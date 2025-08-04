namespace DynamicMenu.Web.Helper
{
    public static class JsonObjectExtensions
    {
        public static IEnumerable<IDictionary<string, object>> ToJson(this IEnumerable<JsonObject> items)
        {
            return items.Select((JsonObject i) => i.ToJson());
        }
    }
}
