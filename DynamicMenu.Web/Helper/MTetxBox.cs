using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace DynamicMenu.Web.Helper
{
    public class MTetxBox : WidgetBase, IFormEditor
    {
        public DataSource DataSource { get; private set; }

        public string DataSourceId { get; set; }

        public string FieldName { get; set; }

        public PopupAnimation Animation { get; private set; }

        public IEnumerable Value { get; set; }

        //
        // Summary:
        //     Allows customization of the title's text in the adaptive view of the component.
        public string AdaptiveTitle { get; set; }

        //
        // Summary:
        //     Allows customization of the subtitle's text in the adaptive view of the component.
        public string AdaptiveSubtitle { get; set; }

        //
        // Summary:
        //     Controls whether to bind the widget to the data source on initialization.
        public bool? AutoBind { get; set; }

        //
        // Summary:
        //     Controls whether to close the widget suggestion list on item selection.
        public bool? AutoClose { get; set; }

        //
        // Summary:
        //     If set to true, the widget automatically adjusts the width of the popup element
        //     and does not wrap up the item label.
        public bool? AutoWidth { get; set; }

        //
        // Summary:
        //     Unless this options is set to false, a button will appear when hovering the widget.
        //     Clicking that button will reset the widget's value and will trigger the change
        //     event.
        public bool? ClearButton { get; set; }

        //
        // Summary:
        //     The field of the data item that provides the text content of the list items.
        //     The widget will filter the data source based on this field.
        public string DataTextField { get; set; }

        //
        // Summary:
        //     The field of the data item that provides the value of the widget.
        public string DataValueField { get; set; }

        //
        // Summary:
        //     Specifies the delay in milliseconds after which the MTetxBox will start filtering
        //     dataSource.
        public double? Delay { get; set; }

        //
        // Summary:
        //     Configures MTetxBox to render a down arrow that opens and closes its popup.
        public bool? DownArrow { get; set; }

        //
        // Summary:
        //     If set to false the widget will be disabled and will not allow user input. The
        //     widget is enabled by default and allows user input.
        public bool? Enable { get; set; }

        //
        // Summary:
        //     If set to true the widget will not show all items when the text of the search
        //     input cleared. By default the widget shows all items when the text of the search
        //     input is cleared. Works in conjunction with minLength.
        public bool? EnforceMinLength { get; set; }

        //
        // Summary:
        //     The template used to render the fixed header group. By default the widget displays
        //     only the value of the current group.
        public string FixedGroupTemplate { get; set; }

        //
        // Summary:
        //     The id of the script element used for FixedGroupTemplate
        public string FixedGroupTemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for FixedGroupTemplate
        public string FixedGroupTemplateHandler { get; set; }

        //
        // Summary:
        //     The template used to render the footer template. The footer template receives
        //     the widget itself as a part of the data argument. Use the widget fields directly
        //     in the template.
        public string FooterTemplate { get; set; }

        //
        // Summary:
        //     The id of the script element used for FooterTemplate
        public string FooterTemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for FooterTemplate
        public string FooterTemplateHandler { get; set; }

        //
        // Summary:
        //     Specifies the inputmode attribute of the inner <input /> element. It is used
        //     to specify the type of on-screen keyboard that should be displayed when the user
        //     focuses the input.
        public string InputMode { get; set; }

        //
        // Summary:
        //     Adds a label before the input. If the input has no id attribute, a generated
        //     id will be assigned. The string and the function parameters are setting the inner
        //     HTML of the label.
        public MTetxBoxLabelSettings Label { get; } = new MTetxBoxLabelSettings();


        //
        // Summary:
        //     The template used to render the groups. By default the widget displays only the
        //     value of the group.
        public string GroupTemplate { get; set; }

        //
        // Summary:
        //     The id of the script element used for GroupTemplate
        public string GroupTemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for GroupTemplate
        public string GroupTemplateHandler { get; set; }

        //
        // Summary:
        //     The height of the suggestion popup in pixels. The default value is 200 pixels.
        public double? Height { get; set; }

        //
        // Summary:
        //     If set to true the first suggestion will be automatically highlighted.
        public bool? HighlightFirst { get; set; }

        //
        // Summary:
        //     If set to false case-sensitive search will be performed to find suggestions.
        //     The widget performs case-insensitive searching by default.
        public bool? IgnoreCase { get; set; }

        //
        // Summary:
        //     The text messages displayed in the widget. Use this option to customize or localize
        //     the messages.
        public MTetxBoxMessagesSettings Messages { get; } = new MTetxBoxMessagesSettings();


        //
        // Summary:
        //     The minimum number of characters the user must type before a search is performed.
        //     Set to a higher value if the search could match a lot of items. A zero value
        //     means that a request will be made as soon as the user focuses the widget.
        public double? MinLength { get; set; }

        //
        // Summary:
        //     Defines the limit of the selected items. If set to null widget will not limit
        //     number of the selected items.
        public double? MaxSelectedItems { get; set; }

        //
        // Summary:
        //     The template used to render the "no data" template, which will be displayed if
        //     no results are found or the underlying data source is empty. The noData template
        //     receives the widget itself as a part of the data argument. The template will
        //     be evaluated on every widget data bound.
        public string NoDataTemplate { get; set; }

        //
        // Summary:
        //     The id of the script element used for NoDataTemplate
        public string NoDataTemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for NoDataTemplate
        public string NoDataTemplateHandler { get; set; }

        //
        // Summary:
        //     The hint displayed by the widget when it is empty. Not set by default.
        public string Placeholder { get; set; }

        //
        // Summary:
        //     The options that will be used for the popup initialization. For more details
        //     about the available options refer to Popup documentation.
        public MTetxBoxPopupSettings Popup { get; } = new MTetxBoxPopupSettings();


        //
        // Summary:
        //     Specifies a static HTML content, which will be rendered as a header of the popup
        //     element.
        public string HeaderTemplate { get; set; }

        //
        // Summary:
        //     The id of the script element used for HeaderTemplate
        public string HeaderTemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for HeaderTemplate
        public string HeaderTemplateHandler { get; set; }

        //
        // Summary:
        //     The template used to render the items in the popup list.
        public string ItemTemplate { get; set; }

        //
        // Summary:
        //     The id of the script element used for ItemTemplate
        public string ItemTemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for ItemTemplate
        public string ItemTemplateHandler { get; set; }

        //
        // Summary:
        //     The configuration for the prefix adornment of the component.
        public MTetxBoxPrefixOptionsSettings PrefixOptions { get; } = new MTetxBoxPrefixOptionsSettings();


        //
        // Summary:
        //     The configuration for the suffix adornment of the component.
        public MTetxBoxSuffixOptionsSettings SuffixOptions { get; } = new MTetxBoxSuffixOptionsSettings();


        //
        // Summary:
        //     The template used to render the tags.
        public string TagTemplate { get; set; }

        //
        // Summary:
        //     The id of the script element used for TagTemplate
        public string TagTemplateId { get; set; }

        //
        // Summary:
        //     The handler that returns the template used for TagTemplate
        public string TagTemplateHandler { get; set; }

        //
        // Summary:
        //     Represents available tag modes of MTetxBox.
        public MTetxBoxTagMode? TagMode { get; set; }

        //
        // Summary:
        //     Specifies the value binding behavior for the widget. If set to true, the View-Model
        //     field will be updated with the selected item value field. If set to false, the
        //     View-Model field will be updated with the selected item.
        public bool? ValuePrimitive { get; set; }

        //
        // Summary:
        //     Enables the virtualization feature of the widget. The configuration can be set
        //     on an object, which contains two properties - itemHeight and valueMapper.For
        //     detailed information, refer to the article on virtualization.
        public MTetxBoxVirtualSettings Virtual { get; } = new MTetxBoxVirtualSettings();


        //
        // Summary:
        //     The filtering method used to determine the suggestions for the current value.
        public FilterType? Filter { get; set; }

        //
        // Summary:
        //     Sets the size of the component.
        public ComponentSize? Size { get; set; }

        //
        // Summary:
        //     Sets a value controlling the border radius.
        public Rounded? Rounded { get; set; }

        //
        // Summary:
        //     Sets a value controlling how the color is applied.
        public FillMode? FillMode { get; set; }

        //
        // Summary:
        //     Specifies the adaptive rendering of the component.
        public AdaptiveMode? AdaptiveMode { get; set; }

        public MTetxBox(ViewContext viewContext, string fieldName = "")
            : base(viewContext)
        {
            DataSource = new DataSource(base.ModelMetadataProvider);
            Animation = new PopupAnimation();
            FieldName = fieldName;
        }

        protected override TagBuilder GetElement()
        {
            if (Enable == false)
            {
                base.HtmlAttributes.Merge("disabled", "disabled", replaceExisting: true);
            }

            return base.Generator.GenerateMTetxBox(base.ViewContext, base.Explorer, Id, base.Name, base.HtmlAttributes);
        }

        protected override void WriteHtml(TextWriter writer)
        {
            GetElement().WriteTo(writer, base.HtmlEncoder);
            base.WriteHtml(writer);
        }

        public override IDictionary<string, object> Serialize()
        {
            Dictionary<string, object> dictionary = SerializeSettings();
            IDictionary<string, object> dictionary2 = Animation.ToJson();
            if (dictionary2.Keys.Any())
            {
                dictionary["animation"] = dictionary2["animation"];
            }

            if (DataSourceId.HasValue())
            {
                dictionary["dataSourceId"] = DataSourceId;
            }
            else
            {
                if (string.IsNullOrEmpty(base.Name) && !string.IsNullOrEmpty(FieldName))
                {
                    base.Name = FieldName;
                }

                if (DataSource.ServerFiltering && !DataSource.Transport.Read.Data.HasValue())
                {
                    DataSource.Transport.Read.Data = new ClientHandlerDescriptor
                    {
                        HandlerName = "function() { return kendo.ui.MTetxBox.requestData(jQuery(\"" + RegexExtensions.EscapeRegex.Replace(base.Selector, "\\\\$1") + "\")); }"
                    };
                }

                if (!string.IsNullOrEmpty(DataSource.Transport.Read.Url) || !string.IsNullOrEmpty(DataSource.Transport.Read.ActionName) || DataSource.Type.GetValueOrDefault() == DataSourceType.Custom)
                {
                    dictionary["dataSource"] = DataSource.ToJson();
                }
                else if (DataSource.Data != null)
                {
                    dictionary["dataSource"] = DataSource.Data;
                }
            }

            IEnumerable value = GetValue();
            if (value != null)
            {
                dictionary["value"] = value;
            }

            return dictionary;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            writer.Write(base.Initializer.Initialize(base.Selector, "MTetxBox", Serialize()));
        }

        private IEnumerable GetValue()
        {
            if (!string.IsNullOrEmpty(base.Name) && Value == null && base.ViewContext.ViewData.ModelState.TryGetValue(base.Name, out ModelStateEntry value) && value.RawValue != null)
            {
                return value.RawValue as IEnumerable;
            }

            if (!string.IsNullOrEmpty(base.Name) && Value == null)
            {
                return base.ViewContext.ViewData.Eval(base.Name) as IEnumerable;
            }

            return Value;
        }

        //
        // Summary:
        //     Serialize current instance to Dictionary
        protected override Dictionary<string, object> SerializeSettings()
        {
            Dictionary<string, object> dictionary = base.SerializeSettings();
            string adaptiveTitle = AdaptiveTitle;
            if (adaptiveTitle != null && adaptiveTitle.HasValue())
            {
                dictionary["adaptiveTitle"] = AdaptiveTitle;
            }

            string adaptiveSubtitle = AdaptiveSubtitle;
            if (adaptiveSubtitle != null && adaptiveSubtitle.HasValue())
            {
                dictionary["adaptiveSubtitle"] = AdaptiveSubtitle;
            }

            if (AutoBind.HasValue)
            {
                dictionary["autoBind"] = AutoBind;
            }

            if (AutoClose.HasValue)
            {
                dictionary["autoClose"] = AutoClose;
            }

            if (AutoWidth.HasValue)
            {
                dictionary["autoWidth"] = AutoWidth;
            }

            if (ClearButton.HasValue)
            {
                dictionary["clearButton"] = ClearButton;
            }

            string dataTextField = DataTextField;
            if (dataTextField != null && dataTextField.HasValue())
            {
                dictionary["dataTextField"] = DataTextField;
            }

            string dataValueField = DataValueField;
            if (dataValueField != null && dataValueField.HasValue())
            {
                dictionary["dataValueField"] = DataValueField;
            }

            if (Delay.HasValue)
            {
                dictionary["delay"] = Delay;
            }

            if (DownArrow.HasValue)
            {
                dictionary["downArrow"] = DownArrow;
            }

            if (Enable.HasValue)
            {
                dictionary["enable"] = Enable;
            }

            if (EnforceMinLength.HasValue)
            {
                dictionary["enforceMinLength"] = EnforceMinLength;
            }

            if (FixedGroupTemplateId.HasValue())
            {
                dictionary["fixedGroupTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{base.IdPrefix}{FixedGroupTemplateId}').html()"
                };
            }
            else if (FixedGroupTemplateHandler.HasValue())
            {
                dictionary["fixedGroupTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = FixedGroupTemplateHandler
                };
            }
            else if (FixedGroupTemplate.HasValue())
            {
                dictionary["fixedGroupTemplate"] = FixedGroupTemplate;
            }

            if (FooterTemplateId.HasValue())
            {
                dictionary["footerTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{base.IdPrefix}{FooterTemplateId}').html()"
                };
            }
            else if (FooterTemplateHandler.HasValue())
            {
                dictionary["footerTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = FooterTemplateHandler
                };
            }
            else if (FooterTemplate.HasValue())
            {
                dictionary["footerTemplate"] = FooterTemplate;
            }

            string inputMode = InputMode;
            if (inputMode != null && inputMode.HasValue())
            {
                dictionary["inputMode"] = InputMode;
            }

            Dictionary<string, object> dictionary2 = Label.Serialize();
            if (dictionary2.Any())
            {
                dictionary["label"] = dictionary2;
            }

            if (GroupTemplateId.HasValue())
            {
                dictionary["groupTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{base.IdPrefix}{GroupTemplateId}').html()"
                };
            }
            else if (GroupTemplateHandler.HasValue())
            {
                dictionary["groupTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = GroupTemplateHandler
                };
            }
            else if (GroupTemplate.HasValue())
            {
                dictionary["groupTemplate"] = GroupTemplate;
            }

            if (Height.HasValue)
            {
                dictionary["height"] = Height;
            }

            if (HighlightFirst.HasValue)
            {
                dictionary["highlightFirst"] = HighlightFirst;
            }

            if (IgnoreCase.HasValue)
            {
                dictionary["ignoreCase"] = IgnoreCase;
            }

            Dictionary<string, object> dictionary3 = Messages.Serialize();
            if (dictionary3.Any())
            {
                dictionary["messages"] = dictionary3;
            }

            if (MinLength.HasValue)
            {
                dictionary["minLength"] = MinLength;
            }

            if (MaxSelectedItems.HasValue)
            {
                dictionary["maxSelectedItems"] = MaxSelectedItems;
            }

            if (NoDataTemplateId.HasValue())
            {
                dictionary["noDataTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{base.IdPrefix}{NoDataTemplateId}').html()"
                };
            }
            else if (NoDataTemplateHandler.HasValue())
            {
                dictionary["noDataTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = NoDataTemplateHandler
                };
            }
            else if (NoDataTemplate.HasValue())
            {
                dictionary["noDataTemplate"] = NoDataTemplate;
            }

            string placeholder = Placeholder;
            if (placeholder != null && placeholder.HasValue())
            {
                dictionary["placeholder"] = Placeholder;
            }

            Dictionary<string, object> dictionary4 = Popup.Serialize();
            if (dictionary4.Any())
            {
                dictionary["popup"] = dictionary4;
            }

            if (HeaderTemplateId.HasValue())
            {
                dictionary["headerTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{base.IdPrefix}{HeaderTemplateId}').html()"
                };
            }
            else if (HeaderTemplateHandler.HasValue())
            {
                dictionary["headerTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = HeaderTemplateHandler
                };
            }
            else if (HeaderTemplate.HasValue())
            {
                dictionary["headerTemplate"] = HeaderTemplate;
            }

            if (ItemTemplateId.HasValue())
            {
                dictionary["itemTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{base.IdPrefix}{ItemTemplateId}').html()"
                };
            }
            else if (ItemTemplateHandler.HasValue())
            {
                dictionary["itemTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = ItemTemplateHandler
                };
            }
            else if (ItemTemplate.HasValue())
            {
                dictionary["itemTemplate"] = ItemTemplate;
            }

            Dictionary<string, object> dictionary5 = PrefixOptions.Serialize();
            if (dictionary5.Any())
            {
                dictionary["prefixOptions"] = dictionary5;
            }

            Dictionary<string, object> dictionary6 = SuffixOptions.Serialize();
            if (dictionary6.Any())
            {
                dictionary["suffixOptions"] = dictionary6;
            }

            if (TagTemplateId.HasValue())
            {
                dictionary["tagTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = $"jQuery('{base.IdPrefix}{TagTemplateId}').html()"
                };
            }
            else if (TagTemplateHandler.HasValue())
            {
                dictionary["tagTemplate"] = new ClientHandlerDescriptor
                {
                    HandlerName = TagTemplateHandler
                };
            }
            else if (TagTemplate.HasValue())
            {
                dictionary["tagTemplate"] = TagTemplate;
            }

            if (TagMode.HasValue)
            {
                dictionary["tagMode"] = TagMode?.Serialize();
            }

            if (ValuePrimitive.HasValue)
            {
                dictionary["valuePrimitive"] = ValuePrimitive;
            }

            Dictionary<string, object> dictionary7 = Virtual.Serialize();
            if (dictionary7.Any())
            {
                dictionary["virtual"] = dictionary7;
            }
            else if (Virtual.Enabled.HasValue)
            {
                dictionary["virtual"] = Virtual.Enabled;
            }

            if (Filter.HasValue)
            {
                dictionary["filter"] = Filter?.Serialize();
            }

            if (Size.HasValue)
            {
                dictionary["size"] = Size?.Serialize();
            }

            if (Rounded.HasValue)
            {
                dictionary["rounded"] = Rounded?.Serialize();
            }

            if (FillMode.HasValue)
            {
                dictionary["fillMode"] = FillMode?.Serialize();
            }

            if (AdaptiveMode.HasValue)
            {
                dictionary["adaptiveMode"] = AdaptiveMode?.Serialize();
            }

            return dictionary;
        }
    }

}
