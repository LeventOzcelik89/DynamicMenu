namespace DynamicMenu.Web.Helper
{
    public class ClientHandlerDescriptor
    {
        //
        // Summary:
        //     A Razor template delegate.
        public Func<object, object> TemplateDelegate { get; set; }

        //
        // Summary:
        //     The name of the JavaScript function which will be called as a handler.
        public string HandlerName { get; set; }

        public bool HasValue()
        {
            if (!HandlerName.HasValue())
            {
                return TemplateDelegate != null;
            }

            return true;
        }
    }
}
