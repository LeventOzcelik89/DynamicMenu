namespace DynamicMenu.Web.Helper
{
    public interface IJavaScriptInitializer
    {
        IJavaScriptSerializer CreateSerializer();

        string Initialize(string id, string name, IDictionary<string, object> options);

        string InitializeFor(string selector, string name, IDictionary<string, object> options);

        string InitializeFor(string selector, string name, IDictionary<string, object> options, bool isChildComponent);

        string Serialize(IDictionary<string, object> @object, char quotes = '"');
    }
}
