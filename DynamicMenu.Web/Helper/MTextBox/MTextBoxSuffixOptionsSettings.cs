namespace DynamicMenu.Web.Helper
{
    public class MTextBoxSuffixOptionsSettings<T> where T : struct
    {
        //
        // Summary:
        //     Defines the name for an existing icon in a Kendo UI theme or SVG content
        public string Icon { get; set; }

        //
        // Summary:
        //     The template for the suffix adornment of the widget.
        public string Template { get; set; }

        //
        // Summary:
        //     The id of the script element used for Template
        public string TemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for Template
        public string TemplateHandler { get; set; }

        //
        // Summary:
        //     If set to false, the suffix adornment will not have a separator.
        public bool? Separator { get; set; }

        public MTextBox<T> NumericTextBox { get; set; }

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
            string icon = Icon;
            if (icon != null && icon.HasValue())
            {
                dictionary["icon"] = Icon;
            }

            if (TemplateId.HasValue())
            {
                dictionary["template"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{NumericTextBox.IdPrefix}{TemplateId}').html()"
                };
            }
            else if (TemplateHandler.HasValue())
            {
                dictionary["template"] = new ClientHandlerDescriptor
                {
                    HandlerName = TemplateHandler
                };
            }
            else if (Template.HasValue())
            {
                dictionary["template"] = Template;
            }

            if (Separator.HasValue)
            {
                dictionary["separator"] = Separator;
            }

            return dictionary;
        }
    }
}
