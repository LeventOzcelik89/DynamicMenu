namespace DynamicMenu.Web.Helper
{
    public class MTextBoxLabelSettings<T> where T : struct
    {
        //
        // Summary:
        //     Sets the inner HTML of the label.
        public string Content { get; set; }

        public ClientHandlerDescriptor ContentHandler { get; set; }

        //
        // Summary:
        //     If set to true, the widget will be wrapped in a container that will allow the
        //     floating label functionality.
        public bool? Floating { get; set; }

        public MTextBox<T> MTextBox { get; set; }

        //
        // Summary:
        //     Serialize current instance to Dictionary
        public Dictionary<string, object> Serialize()
        {
            return SerializeSettings();
        }

        //
        // Summary:
        //     Serialize current instance to Dictionary
        protected virtual Dictionary<string, object> SerializeSettings()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            ClientHandlerDescriptor contentHandler = ContentHandler;
            if (contentHandler != null && contentHandler.HasValue())
            {
                dictionary["content"] = ContentHandler;
            }
            else
            {
                string content = Content;
                if (content != null && content.HasValue())
                {
                    dictionary["content"] = Content;
                }
            }

            if (Floating.HasValue)
            {
                dictionary["floating"] = Floating;
            }

            return dictionary;
        }
    }
}
