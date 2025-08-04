using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.IO;

namespace DynamicMenu.Web.Helper
{
    public class MTextBoxBuilder : MvvmWidgetBuilderBase<MultiSelect, MTextBoxBuilder>
    {
        public MTextBoxBuilder(MultiSelect component)
            : base(component)
        {
        }

        //
        // Summary:
        //     Use to enable or disable animation of the popup element.
        //
        // Parameters:
        //   enable:
        //     The boolean value.
        public MTextBoxBuilder Animation(bool enable)
        {
            base.Component.Animation.Enabled = enable;
            return this;
        }

        //
        // Summary:
        //     Configures the animation effects of the widget.
        //
        // Parameters:
        //   animationAction:
        //     The action which configures the animation effects.
        public MTextBoxBuilder Animation(Action<PopupAnimationBuilder> animationAction)
        {
            animationAction(new PopupAnimationBuilder(base.Component.Animation));
            return this;
        }

        //
        // Summary:
        //     Binds the MultiSelect to an IEnumerable list.
        //
        // Parameters:
        //   dataSource:
        //     The data source.
        public MTextBoxBuilder BindTo(IEnumerable data)
        {
            base.Component.DataSource.Data = data;
            return this;
        }

        //
        // Summary:
        //     Binds the MultiSelect to a list of SelectListItem.
        //
        // Parameters:
        //   dataSource:
        //     The data source.
        public MTextBoxBuilder BindTo(IEnumerable<SelectListItem> dataSource)
        {
            if (string.IsNullOrEmpty(base.Component.DataValueField) && string.IsNullOrEmpty(base.Component.DataTextField))
            {
                DataValueField("Value");
                DataTextField("Text");
            }

            base.Component.DataSource.Data = dataSource.Select((SelectListItem item) => new
            {
                Text = item.Text,
                Value = (item.Value ?? item.Text),
                Selected = item.Selected
            });
            if (base.Component.Value == null)
            {
                base.Component.Value = from item in dataSource
                                       where item.Selected
                                       select item.Value ?? item.Text;
            }

            return this;
        }

        //
        // Summary:
        //     Defines the items in the MultiSelect
        //
        // Parameters:
        //   addAction:
        //     The add action.
        public MTextBoxBuilder Items(Action<SelectListItemFactory> addAction)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            addAction(new SelectListItemFactory(list));
            return BindTo(list);
        }

        //
        // Summary:
        //     Sets the data source configuration of the MultiSelect.
        //
        // Parameters:
        //   configurator:
        //     The lambda which configures the data source
        public MTextBoxBuilder DataSource(Action<ReadOnlyDataSourceBuilder> configurator)
        {
            configurator(new ReadOnlyDataSourceBuilder(base.Component.DataSource, base.Component.ViewContext, base.Component.UrlGenerator));
            return this;
        }

        public MTextBoxBuilder DataSource(string dataSourceId)
        {
            base.Component.DataSourceId = dataSourceId;
            return this;
        }

        //
        // Summary:
        //     Use it to enable filtering of items. The supported filter values are startswith,
        //     endswith and contains.
        //
        // Parameters:
        //   value:
        //     The value for Filter
        [EditorBrowsable(EditorBrowsableState.Never)]
        public MTextBoxBuilder Filter(string value)
        {
            base.Container.Filter = (FilterType)Enum.Parse(typeof(FilterType), value, ignoreCase: true);
            return this;
        }

        //
        // Summary:
        //     Sets the value of the widget.
        //
        // Parameters:
        //   addAction:
        //     The collection of values.
        public MTextBoxBuilder Value(IEnumerable value)
        {
            base.Component.Value = value;
            return this;
        }

        //
        // Summary:
        //     Allows customization of the title's text in the adaptive view of the component.
        //
        //
        // Parameters:
        //   value:
        //     The value for AdaptiveTitle
        public MTextBoxBuilder AdaptiveTitle(string value)
        {
            base.Container.AdaptiveTitle = value;
            return this;
        }

        //
        // Summary:
        //     Allows customization of the subtitle's text in the adaptive view of the component.
        //
        //
        // Parameters:
        //   value:
        //     The value for AdaptiveSubtitle
        public MTextBoxBuilder AdaptiveSubtitle(string value)
        {
            base.Container.AdaptiveSubtitle = value;
            return this;
        }

        //
        // Summary:
        //     Controls whether to bind the widget to the data source on initialization.
        //
        // Parameters:
        //   value:
        //     The value for AutoBind
        public MTextBoxBuilder AutoBind(bool value)
        {
            base.Container.AutoBind = value;
            return this;
        }

        //
        // Summary:
        //     Controls whether to close the widget suggestion list on item selection.
        //
        // Parameters:
        //   value:
        //     The value for AutoClose
        public MTextBoxBuilder AutoClose(bool value)
        {
            base.Container.AutoClose = value;
            return this;
        }

        //
        // Summary:
        //     If set to true, the widget automatically adjusts the width of the popup element
        //     and does not wrap up the item label.
        //
        // Parameters:
        //   value:
        //     The value for AutoWidth
        public MTextBoxBuilder AutoWidth(bool value)
        {
            base.Container.AutoWidth = value;
            return this;
        }

        //
        // Summary:
        //     Unless this options is set to false, a button will appear when hovering the widget.
        //     Clicking that button will reset the widget's value and will trigger the change
        //     event.
        //
        // Parameters:
        //   value:
        //     The value for ClearButton
        public MTextBoxBuilder ClearButton(bool value)
        {
            base.Container.ClearButton = value;
            return this;
        }

        //
        // Summary:
        //     The field of the data item that provides the text content of the list items.
        //     The widget will filter the data source based on this field.
        //
        // Parameters:
        //   value:
        //     The value for DataTextField
        public MTextBoxBuilder DataTextField(string value)
        {
            base.Container.DataTextField = value;
            return this;
        }

        //
        // Summary:
        //     The field of the data item that provides the value of the widget.
        //
        // Parameters:
        //   value:
        //     The value for DataValueField
        public MTextBoxBuilder DataValueField(string value)
        {
            base.Container.DataValueField = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the delay in milliseconds after which the MultiSelect will start filtering
        //     dataSource.
        //
        // Parameters:
        //   value:
        //     The value for Delay
        public MTextBoxBuilder Delay(double value)
        {
            base.Container.Delay = value;
            return this;
        }

        //
        // Summary:
        //     Configures MultiSelect to render a down arrow that opens and closes its popup.
        //
        //
        // Parameters:
        //   value:
        //     The value for DownArrow
        public MTextBoxBuilder DownArrow(bool value)
        {
            base.Container.DownArrow = value;
            return this;
        }

        //
        // Summary:
        //     Configures MultiSelect to render a down arrow that opens and closes its popup.
        public MTextBoxBuilder DownArrow()
        {
            base.Container.DownArrow = true;
            return this;
        }

        //
        // Summary:
        //     If set to false the widget will be disabled and will not allow user input. The
        //     widget is enabled by default and allows user input.
        //
        // Parameters:
        //   value:
        //     The value for Enable
        public MTextBoxBuilder Enable(bool value)
        {
            base.Container.Enable = value;
            return this;
        }

        //
        // Summary:
        //     If set to true the widget will not show all items when the text of the search
        //     input cleared. By default the widget shows all items when the text of the search
        //     input is cleared. Works in conjunction with minLength.
        //
        // Parameters:
        //   value:
        //     The value for EnforceMinLength
        public MTextBoxBuilder EnforceMinLength(bool value)
        {
            base.Container.EnforceMinLength = value;
            return this;
        }

        //
        // Summary:
        //     If set to true the widget will not show all items when the text of the search
        //     input cleared. By default the widget shows all items when the text of the search
        //     input is cleared. Works in conjunction with minLength.
        public MTextBoxBuilder EnforceMinLength()
        {
            base.Container.EnforceMinLength = true;
            return this;
        }

        //
        // Summary:
        //     The template used to render the fixed header group. By default the widget displays
        //     only the value of the current group.
        //
        // Parameters:
        //   value:
        //     The value for FixedGroupTemplate
        public MTextBoxBuilder FixedGroupTemplate(string value)
        {
            base.Container.FixedGroupTemplate = value;
            return this;
        }

        //
        // Summary:
        //     The template used to render the fixed header group. By default the widget displays
        //     only the value of the current group.
        //
        // Parameters:
        //   templateId:
        //     The ID of the template element for FixedGroupTemplate
        public MTextBoxBuilder FixedGroupTemplateId(string templateId)
        {
            base.Container.FixedGroupTemplateId = templateId;
            return this;
        }

        //
        // Summary:
        //     The template used to render the fixed header group. By default the widget displays
        //     only the value of the current group.
        //
        // Parameters:
        //   templateView:
        //     The view that contains the template for FixedGroupTemplate
        public MTextBoxBuilder FixedGroupTemplateView(IHtmlContent templateView)
        {
            base.Container.FixedGroupTemplate = templateView.PartialViewToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the fixed header group. By default the widget displays
        //     only the value of the current group.
        //
        // Parameters:
        //   templateHandler:
        //     The handler that returs the template for FixedGroupTemplate
        public MTextBoxBuilder FixedGroupTemplateHandler(string templateHandler)
        {
            base.Container.FixedGroupTemplateHandler = templateHandler;
            return this;
        }

        //
        // Summary:
        //     The template used to render the fixed header group. By default the widget displays
        //     only the value of the current group.
        //
        // Parameters:
        //   template:
        //     A Template component that configures the fixedgrouptemplate.
        public MTextBoxBuilder FixedGroupTemplate<TModel>(TemplateBuilder<TModel> template)
        {
            base.Container.FixedGroupTemplateHandler = template.ToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the footer template. The footer template receives
        //     the widget itself as a part of the data argument. Use the widget fields directly
        //     in the template.
        //
        // Parameters:
        //   value:
        //     The value for FooterTemplate
        public MTextBoxBuilder FooterTemplate(string value)
        {
            base.Container.FooterTemplate = value;
            return this;
        }

        //
        // Summary:
        //     The template used to render the footer template. The footer template receives
        //     the widget itself as a part of the data argument. Use the widget fields directly
        //     in the template.
        //
        // Parameters:
        //   templateId:
        //     The ID of the template element for FooterTemplate
        public MTextBoxBuilder FooterTemplateId(string templateId)
        {
            base.Container.FooterTemplateId = templateId;
            return this;
        }

        //
        // Summary:
        //     The template used to render the footer template. The footer template receives
        //     the widget itself as a part of the data argument. Use the widget fields directly
        //     in the template.
        //
        // Parameters:
        //   templateView:
        //     The view that contains the template for FooterTemplate
        public MTextBoxBuilder FooterTemplateView(IHtmlContent templateView)
        {
            base.Container.FooterTemplate = templateView.PartialViewToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the footer template. The footer template receives
        //     the widget itself as a part of the data argument. Use the widget fields directly
        //     in the template.
        //
        // Parameters:
        //   templateHandler:
        //     The handler that returs the template for FooterTemplate
        public MTextBoxBuilder FooterTemplateHandler(string templateHandler)
        {
            base.Container.FooterTemplateHandler = templateHandler;
            return this;
        }

        //
        // Summary:
        //     The template used to render the footer template. The footer template receives
        //     the widget itself as a part of the data argument. Use the widget fields directly
        //     in the template.
        //
        // Parameters:
        //   template:
        //     A Template component that configures the footertemplate.
        public MTextBoxBuilder FooterTemplate<TModel>(TemplateBuilder<TModel> template)
        {
            base.Container.FooterTemplateHandler = template.ToString();
            return this;
        }

        //
        // Summary:
        //     Specifies the inputmode attribute of the inner <input /> element. It is used
        //     to specify the type of on-screen keyboard that should be displayed when the user
        //     focuses the input.
        //
        // Parameters:
        //   value:
        //     The value for InputMode
        public MTextBoxBuilder InputMode(string value)
        {
            base.Container.InputMode = value;
            return this;
        }

        //
        // Summary:
        //     Adds a label before the input. If the input has no id attribute, a generated
        //     id will be assigned. The string and the function parameters are setting the inner
        //     HTML of the label.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the label setting.
        public MTextBoxBuilder Label(Action<MultiSelectLabelSettingsBuilder> configurator)
        {
            base.Container.Label.MultiSelect = base.Container;
            configurator(new MultiSelectLabelSettingsBuilder(base.Container.Label));
            return this;
        }

        //
        // Summary:
        //     The template used to render the groups. By default the widget displays only the
        //     value of the group.
        //
        // Parameters:
        //   value:
        //     The value for GroupTemplate
        public MTextBoxBuilder GroupTemplate(string value)
        {
            base.Container.GroupTemplate = value;
            return this;
        }

        //
        // Summary:
        //     The template used to render the groups. By default the widget displays only the
        //     value of the group.
        //
        // Parameters:
        //   templateId:
        //     The ID of the template element for GroupTemplate
        public MTextBoxBuilder GroupTemplateId(string templateId)
        {
            base.Container.GroupTemplateId = templateId;
            return this;
        }

        //
        // Summary:
        //     The template used to render the groups. By default the widget displays only the
        //     value of the group.
        //
        // Parameters:
        //   templateView:
        //     The view that contains the template for GroupTemplate
        public MTextBoxBuilder GroupTemplateView(IHtmlContent templateView)
        {
            base.Container.GroupTemplate = templateView.PartialViewToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the groups. By default the widget displays only the
        //     value of the group.
        //
        // Parameters:
        //   templateHandler:
        //     The handler that returs the template for GroupTemplate
        public MTextBoxBuilder GroupTemplateHandler(string templateHandler)
        {
            base.Container.GroupTemplateHandler = templateHandler;
            return this;
        }

        //
        // Summary:
        //     The template used to render the groups. By default the widget displays only the
        //     value of the group.
        //
        // Parameters:
        //   template:
        //     A Template component that configures the grouptemplate.
        public MTextBoxBuilder GroupTemplate<TModel>(TemplateBuilder<TModel> template)
        {
            base.Container.GroupTemplateHandler = template.ToString();
            return this;
        }

        //
        // Summary:
        //     The height of the suggestion popup in pixels. The default value is 200 pixels.
        //
        //
        // Parameters:
        //   value:
        //     The value for Height
        public MTextBoxBuilder Height(double value)
        {
            base.Container.Height = value;
            return this;
        }

        //
        // Summary:
        //     If set to true the first suggestion will be automatically highlighted.
        //
        // Parameters:
        //   value:
        //     The value for HighlightFirst
        public MTextBoxBuilder HighlightFirst(bool value)
        {
            base.Container.HighlightFirst = value;
            return this;
        }

        //
        // Summary:
        //     If set to false case-sensitive search will be performed to find suggestions.
        //     The widget performs case-insensitive searching by default.
        //
        // Parameters:
        //   value:
        //     The value for IgnoreCase
        public MTextBoxBuilder IgnoreCase(bool value)
        {
            base.Container.IgnoreCase = value;
            return this;
        }

        //
        // Summary:
        //     The text messages displayed in the widget. Use this option to customize or localize
        //     the messages.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the messages setting.
        public MTextBoxBuilder Messages(Action<MultiSelectMessagesSettingsBuilder> configurator)
        {
            base.Container.Messages.MultiSelect = base.Container;
            configurator(new MultiSelectMessagesSettingsBuilder(base.Container.Messages));
            return this;
        }

        //
        // Summary:
        //     The minimum number of characters the user must type before a search is performed.
        //     Set to a higher value if the search could match a lot of items. A zero value
        //     means that a request will be made as soon as the user focuses the widget.
        //
        // Parameters:
        //   value:
        //     The value for MinLength
        public MTextBoxBuilder MinLength(double value)
        {
            base.Container.MinLength = value;
            return this;
        }

        //
        // Summary:
        //     Defines the limit of the selected items. If set to null widget will not limit
        //     number of the selected items.
        //
        // Parameters:
        //   value:
        //     The value for MaxSelectedItems
        public MTextBoxBuilder MaxSelectedItems(double value)
        {
            base.Container.MaxSelectedItems = value;
            return this;
        }

        //
        // Summary:
        //     The template used to render the "no data" template, which will be displayed if
        //     no results are found or the underlying data source is empty. The noData template
        //     receives the widget itself as a part of the data argument. The template will
        //     be evaluated on every widget data bound.
        //
        // Parameters:
        //   value:
        //     The value for NoDataTemplate
        public MTextBoxBuilder NoDataTemplate(string value)
        {
            base.Container.NoDataTemplate = value;
            return this;
        }

        //
        // Summary:
        //     The template used to render the "no data" template, which will be displayed if
        //     no results are found or the underlying data source is empty. The noData template
        //     receives the widget itself as a part of the data argument. The template will
        //     be evaluated on every widget data bound.
        //
        // Parameters:
        //   templateId:
        //     The ID of the template element for NoDataTemplate
        public MTextBoxBuilder NoDataTemplateId(string templateId)
        {
            base.Container.NoDataTemplateId = templateId;
            return this;
        }

        //
        // Summary:
        //     The template used to render the "no data" template, which will be displayed if
        //     no results are found or the underlying data source is empty. The noData template
        //     receives the widget itself as a part of the data argument. The template will
        //     be evaluated on every widget data bound.
        //
        // Parameters:
        //   templateView:
        //     The view that contains the template for NoDataTemplate
        public MTextBoxBuilder NoDataTemplateView(IHtmlContent templateView)
        {
            base.Container.NoDataTemplate = templateView.PartialViewToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the "no data" template, which will be displayed if
        //     no results are found or the underlying data source is empty. The noData template
        //     receives the widget itself as a part of the data argument. The template will
        //     be evaluated on every widget data bound.
        //
        // Parameters:
        //   templateHandler:
        //     The handler that returs the template for NoDataTemplate
        public MTextBoxBuilder NoDataTemplateHandler(string templateHandler)
        {
            base.Container.NoDataTemplateHandler = templateHandler;
            return this;
        }

        //
        // Summary:
        //     The template used to render the "no data" template, which will be displayed if
        //     no results are found or the underlying data source is empty. The noData template
        //     receives the widget itself as a part of the data argument. The template will
        //     be evaluated on every widget data bound.
        //
        // Parameters:
        //   template:
        //     A Template component that configures the nodatatemplate.
        public MTextBoxBuilder NoDataTemplate<TModel>(TemplateBuilder<TModel> template)
        {
            base.Container.NoDataTemplateHandler = template.ToString();
            return this;
        }

        //
        // Summary:
        //     The hint displayed by the widget when it is empty. Not set by default.
        //
        // Parameters:
        //   value:
        //     The value for Placeholder
        public MTextBoxBuilder Placeholder(string value)
        {
            base.Container.Placeholder = value;
            return this;
        }

        //
        // Summary:
        //     The options that will be used for the popup initialization. For more details
        //     about the available options refer to Popup documentation.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the popup setting.
        public MTextBoxBuilder Popup(Action<MultiSelectPopupSettingsBuilder> configurator)
        {
            base.Container.Popup.MultiSelect = base.Container;
            configurator(new MultiSelectPopupSettingsBuilder(base.Container.Popup));
            return this;
        }

        //
        // Summary:
        //     Specifies a static HTML content, which will be rendered as a header of the popup
        //     element.
        //
        // Parameters:
        //   value:
        //     The value for HeaderTemplate
        public MTextBoxBuilder HeaderTemplate(string value)
        {
            base.Container.HeaderTemplate = value;
            return this;
        }

        //
        // Summary:
        //     Specifies a static HTML content, which will be rendered as a header of the popup
        //     element.
        //
        // Parameters:
        //   templateId:
        //     The ID of the template element for HeaderTemplate
        public MTextBoxBuilder HeaderTemplateId(string templateId)
        {
            base.Container.HeaderTemplateId = templateId;
            return this;
        }

        //
        // Summary:
        //     Specifies a static HTML content, which will be rendered as a header of the popup
        //     element.
        //
        // Parameters:
        //   templateView:
        //     The view that contains the template for HeaderTemplate
        public MTextBoxBuilder HeaderTemplateView(IHtmlContent templateView)
        {
            base.Container.HeaderTemplate = templateView.PartialViewToString();
            return this;
        }

        //
        // Summary:
        //     Specifies a static HTML content, which will be rendered as a header of the popup
        //     element.
        //
        // Parameters:
        //   templateHandler:
        //     The handler that returs the template for HeaderTemplate
        public MTextBoxBuilder HeaderTemplateHandler(string templateHandler)
        {
            base.Container.HeaderTemplateHandler = templateHandler;
            return this;
        }

        //
        // Summary:
        //     Specifies a static HTML content, which will be rendered as a header of the popup
        //     element.
        //
        // Parameters:
        //   template:
        //     A Template component that configures the headertemplate.
        public MTextBoxBuilder HeaderTemplate<TModel>(TemplateBuilder<TModel> template)
        {
            base.Container.HeaderTemplateHandler = template.ToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the items in the popup list.
        //
        // Parameters:
        //   value:
        //     The value for ItemTemplate
        public MTextBoxBuilder ItemTemplate(string value)
        {
            base.Container.ItemTemplate = value;
            return this;
        }

        //
        // Summary:
        //     The template used to render the items in the popup list.
        //
        // Parameters:
        //   templateId:
        //     The ID of the template element for ItemTemplate
        public MTextBoxBuilder ItemTemplateId(string templateId)
        {
            base.Container.ItemTemplateId = templateId;
            return this;
        }

        //
        // Summary:
        //     The template used to render the items in the popup list.
        //
        // Parameters:
        //   templateView:
        //     The view that contains the template for ItemTemplate
        public MTextBoxBuilder ItemTemplateView(IHtmlContent templateView)
        {
            base.Container.ItemTemplate = templateView.PartialViewToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the items in the popup list.
        //
        // Parameters:
        //   templateHandler:
        //     The handler that returs the template for ItemTemplate
        public MTextBoxBuilder ItemTemplateHandler(string templateHandler)
        {
            base.Container.ItemTemplateHandler = templateHandler;
            return this;
        }

        //
        // Summary:
        //     The template used to render the items in the popup list.
        //
        // Parameters:
        //   template:
        //     A Template component that configures the itemtemplate.
        public MTextBoxBuilder ItemTemplate<TModel>(TemplateBuilder<TModel> template)
        {
            base.Container.ItemTemplateHandler = template.ToString();
            return this;
        }

        //
        // Summary:
        //     The configuration for the prefix adornment of the component.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the prefixoptions setting.
        public MTextBoxBuilder PrefixOptions(Action<MultiSelectPrefixOptionsSettingsBuilder> configurator)
        {
            base.Container.PrefixOptions.MultiSelect = base.Container;
            configurator(new MultiSelectPrefixOptionsSettingsBuilder(base.Container.PrefixOptions));
            return this;
        }

        //
        // Summary:
        //     The configuration for the suffix adornment of the component.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the suffixoptions setting.
        public MTextBoxBuilder SuffixOptions(Action<MultiSelectSuffixOptionsSettingsBuilder> configurator)
        {
            base.Container.SuffixOptions.MultiSelect = base.Container;
            configurator(new MultiSelectSuffixOptionsSettingsBuilder(base.Container.SuffixOptions));
            return this;
        }

        //
        // Summary:
        //     The template used to render the tags.
        //
        // Parameters:
        //   value:
        //     The value for TagTemplate
        public MTextBoxBuilder TagTemplate(string value)
        {
            base.Container.TagTemplate = value;
            return this;
        }

        //
        // Summary:
        //     The template used to render the tags.
        //
        // Parameters:
        //   templateId:
        //     The ID of the template element for TagTemplate
        public MTextBoxBuilder TagTemplateId(string templateId)
        {
            base.Container.TagTemplateId = templateId;
            return this;
        }

        //
        // Summary:
        //     The template used to render the tags.
        //
        // Parameters:
        //   templateView:
        //     The view that contains the template for TagTemplate
        public MTextBoxBuilder TagTemplateView(IHtmlContent templateView)
        {
            base.Container.TagTemplate = templateView.PartialViewToString();
            return this;
        }

        //
        // Summary:
        //     The template used to render the tags.
        //
        // Parameters:
        //   templateHandler:
        //     The handler that returs the template for TagTemplate
        public MTextBoxBuilder TagTemplateHandler(string templateHandler)
        {
            base.Container.TagTemplateHandler = templateHandler;
            return this;
        }

        //
        // Summary:
        //     The template used to render the tags.
        //
        // Parameters:
        //   template:
        //     A Template component that configures the tagtemplate.
        public MTextBoxBuilder TagTemplate<TModel>(TemplateBuilder<TModel> template)
        {
            base.Container.TagTemplateHandler = template.ToString();
            return this;
        }

        //
        // Summary:
        //     Represents available tag modes of MultiSelect.
        //
        // Parameters:
        //   value:
        //     The value for TagMode
        public MTextBoxBuilder TagMode(MultiSelectTagMode value)
        {
            base.Container.TagMode = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the value binding behavior for the widget. If set to true, the View-Model
        //     field will be updated with the selected item value field. If set to false, the
        //     View-Model field will be updated with the selected item.
        //
        // Parameters:
        //   value:
        //     The value for ValuePrimitive
        public MTextBoxBuilder ValuePrimitive(bool value)
        {
            base.Container.ValuePrimitive = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the value binding behavior for the widget. If set to true, the View-Model
        //     field will be updated with the selected item value field. If set to false, the
        //     View-Model field will be updated with the selected item.
        public MTextBoxBuilder ValuePrimitive()
        {
            base.Container.ValuePrimitive = true;
            return this;
        }

        //
        // Summary:
        //     Enables the virtualization feature of the widget. The configuration can be set
        //     on an object, which contains two properties - itemHeight and valueMapper.For
        //     detailed information, refer to the article on virtualization.
        //
        // Parameters:
        //   configurator:
        //     The configurator for the virtual setting.
        public MTextBoxBuilder Virtual(Action<MultiSelectVirtualSettingsBuilder> configurator)
        {
            base.Container.Virtual.Enabled = true;
            base.Container.Virtual.MultiSelect = base.Container;
            configurator(new MultiSelectVirtualSettingsBuilder(base.Container.Virtual));
            return this;
        }

        //
        // Summary:
        //     Enables the virtualization feature of the widget. The configuration can be set
        //     on an object, which contains two properties - itemHeight and valueMapper.For
        //     detailed information, refer to the article on virtualization.
        public MTextBoxBuilder Virtual()
        {
            base.Container.Virtual.Enabled = true;
            return this;
        }

        //
        // Summary:
        //     Enables the virtualization feature of the widget. The configuration can be set
        //     on an object, which contains two properties - itemHeight and valueMapper.For
        //     detailed information, refer to the article on virtualization.
        //
        // Parameters:
        //   enabled:
        //     Enables or disables the virtual option.
        public MTextBoxBuilder Virtual(bool enabled)
        {
            base.Container.Virtual.Enabled = enabled;
            return this;
        }

        //
        // Summary:
        //     The filtering method used to determine the suggestions for the current value.
        //
        //
        // Parameters:
        //   value:
        //     The value for Filter
        public MTextBoxBuilder Filter(FilterType value)
        {
            base.Container.Filter = value;
            return this;
        }

        //
        // Summary:
        //     Sets the size of the component.
        //
        // Parameters:
        //   value:
        //     The value for Size
        public MTextBoxBuilder Size(ComponentSize value)
        {
            base.Container.Size = value;
            return this;
        }

        //
        // Summary:
        //     Sets a value controlling the border radius.
        //
        // Parameters:
        //   value:
        //     The value for Rounded
        public MTextBoxBuilder Rounded(Rounded value)
        {
            base.Container.Rounded = value;
            return this;
        }

        //
        // Summary:
        //     Sets a value controlling how the color is applied.
        //
        // Parameters:
        //   value:
        //     The value for FillMode
        public MTextBoxBuilder FillMode(FillMode value)
        {
            base.Container.FillMode = value;
            return this;
        }

        //
        // Summary:
        //     Specifies the adaptive rendering of the component.
        //
        // Parameters:
        //   value:
        //     The value for AdaptiveMode
        public MTextBoxBuilder AdaptiveMode(AdaptiveMode value)
        {
            base.Container.AdaptiveMode = value;
            return this;
        }

        //
        // Summary:
        //     Configures the client-side events.
        //
        // Parameters:
        //   configurator:
        //     The client events action.
        public MTextBoxBuilder Events(Action<MultiSelectEventBuilder> configurator)
        {
            configurator(new MultiSelectEventBuilder(base.Container.Events));
            return this;
        }
    }
}
