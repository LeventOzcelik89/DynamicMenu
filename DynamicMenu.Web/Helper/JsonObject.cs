namespace DynamicMenu.Web.Helper
{
    public abstract class JsonObject
    {
        public IDictionary<string, object> ToJson()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            Serialize(dictionary);
            return dictionary;
        }

        protected abstract void Serialize(IDictionary<string, object> json);
    }
}
