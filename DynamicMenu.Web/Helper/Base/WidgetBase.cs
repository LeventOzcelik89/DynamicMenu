using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Reflection.Emit;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace DynamicMenu.Web.Helper
{
    public abstract class WidgetBase : IHtmlAttributesContainer
    {
        internal const string DeferredScripts = "kendo-deferred-scripts";

        internal static readonly string WidgetsKey = "$KendoWidgetsKey$";

        internal const string DeferredStyles = "kendo-deferred-styles";

        internal static readonly string StylesKey = "$KendoStylesKey$";

        private readonly IKendoOptions kendoOptions;

        //
        // Summary:
        //     Gets the client events of the widget.
        //
        // Value:
        //     The client events.
        public IDictionary<string, object> Events { get; private set; }

        //
        // Summary:
        //     Gets the unique ID of the widget
        //
        // Value:
        //     The unique ID of the widget
        public virtual string Id
        {
            get
            {
                string text = (HtmlAttributes.ContainsKey("id") ? ((string)HtmlAttributes["id"]) : ViewContext.GetFullHtmlFieldName(Name));
                if (SanitezeId)
                {
                    text = Generator.SanitizeId(text);
                }

                return text;
            }
        }

        public bool SanitezeId { get; set; } = true;


        public IJavaScriptWriter Writer { get; set; }

        public IJavaScriptInitializer Initializer { get; set; }

        public bool IsInClientTemplate { get; private set; }

        public bool IsSelfInitialized { get; set; }

        public bool HasDeferredInitialization { get; set; }

        //
        // Summary:
        //     Gets the HTML attributes of the widget.
        //
        // Value:
        //     The HTML attributes.
        public IDictionary<string, object> HtmlAttributes { get; set; }

        //
        // Summary:
        //     Set the JavaScript attributes to the initialization script.
        public IDictionary<string, object> ScriptAttributes { get; set; }

        public IHtmlHelper HtmlHelper { get; set; }

        public IModelMetadataProvider ModelMetadataProvider { get; set; }

        //
        // Summary:
        //     Gets or sets the name.
        //
        // Value:
        //     The name.
        public string Name { get; set; }

        //
        // Summary:
        //     Gets or sets the Explorer.
        //
        // Value:
        //     The Explorer.
        public ModelExplorer Explorer { get; set; }

        public string Selector => IdPrefix + Id;

        public string IdPrefix
        {
            get
            {
                if (!IsInClientTemplate)
                {
                    return "#";
                }

                return "\\#";
            }
        }

        //
        // Summary:
        //     Gets or sets the view context to rendering a view.
        //
        // Value:
        //     The view context.
        public ViewContext ViewContext { get; private set; }

        //
        // Summary:
        //     Gets a reference to the ValueProvider for the current ActionContext
        public IValueProvider ValueProvider
        {
            get
            {
                IValueProviderFactory[] array = ViewContext.GetService<IOptions<MvcOptions>>().Value.ValueProviderFactories.ToArray();
                ValueProviderFactoryContext valueProviderFactoryContext = new ValueProviderFactoryContext(ViewContext.GetService<IActionContextAccessor>().ActionContext);
                for (int i = 0; i < array.Length; i++)
                {
                    array[i].CreateValueProviderAsync(valueProviderFactoryContext);
                }

                return new CompositeValueProvider(valueProviderFactoryContext.ValueProviders);
            }
        }

        protected IKendoHtmlGenerator Generator { get; set; }

        public IUrlGenerator UrlGenerator { get; set; }

        public HtmlEncoder HtmlEncoder { get; set; }

        public bool HasClientComponent { get; set; } = true;

        public bool UseMvvmInitialization { get; set; }

        public WidgetBase(ViewContext viewContext)
        {
            Events = new Dictionary<string, object>();
            HtmlAttributes = new RouteValueDictionary();
            ScriptAttributes = new Dictionary<string, object> { { "type", null } };
            IsSelfInitialized = true;
            Initializer = new JavaScriptInitializer();
            Writer = new JavaScriptWriter();
            ViewContext = viewContext;
            kendoOptions = GetService<IKendoOptions>();
            HasDeferredInitialization = kendoOptions?.DeferToScriptFiles ?? false;
            IKendoOptions obj = kendoOptions;
            if (obj != null && obj.RenderAsModule)
            {
                ScriptAttributes["type"] = "module";
            }

            if (HasDeferredInitialization)
            {
                AppendWidgetToContext();
            }

            Activate();
        }

        //
        // Summary:
        //     Renders the component.
        public void Render()
        {
            RenderHtml(ViewContext.Writer);
        }

        public virtual IDictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>(Events);
        }

        //
        // Summary:
        //     Serialize manual settings here
        protected virtual Dictionary<string, object> SerializeSettings()
        {
            return new Dictionary<string, object>(Events);
        }

        public HtmlString ToClientTemplate()
        {
            IsInClientTemplate = true;
            string input = ToHtmlString().Replace("</script>", "<\\/script>");
            input = RegexExtensions.UnicodeEntityExpression.Replace(input, (Match m) => WebUtility.HtmlDecode(Regex.Unescape("\\u" + m.Groups[1].Value + "#" + m.Groups[2].Value)));
            input = WebUtility.HtmlDecode(input);
            return new HtmlString(input);
        }

        public string ToHtmlString()
        {
            using StringWriter stringWriter = new StringWriter();
            RenderHtml(stringWriter);
            return stringWriter.ToString();
        }

        public virtual void VerifySettings()
        {
            if (!Name.Contains("<#=") && Name.IndexOf(" ", StringComparison.Ordinal) != -1)
            {
                throw new InvalidOperationException(Exceptions.NameCannotContainSpaces);
            }

            this.ThrowIfClassIsPresent("k-" + GetType().Name.ToLowerInvariant() + "-rtl", Exceptions.Rtl);
        }

        //
        // Summary:
        //     Writes the initialization script.
        //
        // Parameters:
        //   writer:
        //     The writer.
        public abstract void WriteInitializationScript(TextWriter writer);

        //
        // Summary:
        //     Writes the deferred styes.
        //
        // Parameters:
        //   writer:
        //     The writer.
        public virtual void WriteDeferredStyles(TextWriter writer)
        {
        }

        public virtual void DeferStyles()
        {
            StringWriter stringWriter = new StringWriter();
            WriteDeferredStyles(stringWriter);
            AppendStylesToContext(stringWriter.ToString());
        }

        public virtual void ProcessSettings()
        {
        }

        public string GetHtml(bool includeScript = true)
        {
            using StringWriter stringWriter = new StringWriter();
            if (!includeScript)
            {
                IsSelfInitialized = false;
            }

            WriteHtml(stringWriter);
            return stringWriter.ToString();
        }

        public string GetInitializator()
        {
            ProcessSettings();
            StringWriter stringWriter = new StringWriter();
            IDictionary<string, object> options = Serialize();
            stringWriter.Write(Initializer.InitializeFor(Selector, GetType().Name.Split('`').First(), options, isChildComponent: true));
            return stringWriter.ToString();
        }

        protected virtual void RenderHtml(TextWriter writer)
        {
            ProcessSettings();
            if (UseMvvmInitialization)
            {
                WriteMvvmHtml(writer);
            }
            else
            {
                WriteHtml(writer);
            }
        }

        protected virtual TagBuilder GetElement()
        {
            throw new NotImplementedException();
        }

        //
        // Summary:
        //     Writes the HTML.
        protected virtual void WriteHtml(TextWriter writer)
        {
            VerifySettings();
            if (IsSelfInitialized && !HasDeferredInitialization)
            {
                if (ScriptAttributes.Where((KeyValuePair<string, object> attribute) => attribute.Value != null).Any())
                {
                    Writer.WriteScriptAttributes(writer, ScriptAttributes);
                }
                else
                {
                    Writer.WriteOpeningTag(writer);
                }

                WriteInitializationScript(writer);
                Writer.WriteClosingTag(writer);
            }

            if (HasDeferredInitialization)
            {
                DeferStyles();
            }
        }

        protected virtual void WriteMvvmHtml(TextWriter writer)
        {
            VerifySettings();
            DefaultJavaScriptSerializer defaultJavaScriptSerializer = new DefaultJavaScriptSerializer();
            TagBuilder element = GetElement();
            IDictionary<string, object> dictionary = Serialize();
            element.Attributes.Add("data-role", GetNameWithoutGenericArity(GetType()).ToLower());
            foreach (KeyValuePair<string, object> item in dictionary)
            {
                string text = item.Key.PascalCaseToKebabCase();
                string text2 = item.Value.ToString();
                bool num = IsEnumerableType(item.Value);
                if (!item.Key.StartsWith("data"))
                {
                    text = "data-" + text;
                }

                if (item.Value is IDictionary<string, object>)
                {
                    text2 = Initializer.Serialize((IDictionary<string, object>)item.Value);
                }

                if (num)
                {
                    text2 = defaultJavaScriptSerializer.Serialize(item.Value);
                }

                if (item.Value is ClientHandlerDescriptor)
                {
                    text2 = (item.Value as ClientHandlerDescriptor).HandlerName;
                }

                if (item.Value is bool)
                {
                    text2 = text2.ToLower();
                }

                element.Attributes.Add(text, text2);
            }

            element.WriteTo(writer, HtmlEncoder);
        }

        public virtual void WriteDeferredScriptInitialization()
        {
            StringWriter stringWriter = new StringWriter();
            WriteInitializationScript(stringWriter);
            AppendScriptToContext(stringWriter.ToString());
        }

        private void Activate()
        {
            Generator = GetService<IKendoHtmlGenerator>();
            HtmlHelper = GetService<IHtmlHelper>();
            HtmlEncoder = GetService<HtmlEncoder>();
            ModelMetadataProvider = GetService<IModelMetadataProvider>();
            UrlGenerator = GetService<IUrlGenerator>();
            ((IViewContextAware)HtmlHelper).Contextualize(ViewContext);
            if (Generator == null)
            {
                throw new Exception("Kendo services are not registered. Please call services.AddKendo() in ConfigureServices method of your project.");
            }
        }

        protected TService GetService<TService>()
        {
            return ViewContext.GetService<TService>();
        }

        internal void RemoveWidgetFromContext()
        {
            IDictionary<object, object> items = ViewContext.HttpContext.Items;
            if (items.ContainsKey(WidgetsKey))
            {
                ((List<WidgetBase>)items[WidgetsKey]).Remove(this);
            }
        }

        private void AppendScriptToContext(string script)
        {
            ViewContext.AppendScriptToContext(script, Id);
        }

        private void AppendWidgetToContext()
        {
            IDictionary<object, object> items = ViewContext.HttpContext.Items;
            List<WidgetBase> list = new List<WidgetBase>();
            if (items.ContainsKey(WidgetsKey))
            {
                list = (List<WidgetBase>)items[WidgetsKey];
            }
            else
            {
                items[WidgetsKey] = list;
            }

            list.Add(this);
        }

        private void AppendStylesToContext(string style)
        {
            ViewContext.AppendStylesToContext(style, Id);
        }

        private string GetNameWithoutGenericArity(Type type)
        {
            string name = type.Name;
            int num = name.IndexOf('`');
            if (num != -1)
            {
                return name.Substring(0, num);
            }

            return name;
        }

        private bool IsEnumerableType(object value)
        {
            Type type = value.GetType();
            if (type.GetInterface("IEnumerable") != null)
            {
                return type != typeof(string);
            }

            return false;
        }
    }
}
