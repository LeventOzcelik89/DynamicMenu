using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;

namespace DynamicMenu.Web.Helper
{
    public class WidgetBuilderBase<TViewComponent, TBuilder> : HelperResult where TViewComponent : WidgetBase where TBuilder : WidgetBuilderBase<TViewComponent, TBuilder>
    {
        //
        // Summary:
        //     Gets the view component.
        //
        // Value:
        //     The component.
        protected internal TViewComponent Component { get; set; }

        //
        // Summary:
        //     Alias for the component as used from settings builder
        //
        // Value:
        //     The settings container.
        protected internal TViewComponent Container => Component;

        private bool HasModelExpression { get; set; }

        //
        // Summary:
        //     Initializes a new instance of the WidgetBuilderBase class.
        //
        // Parameters:
        //   component:
        //     The component.
        protected WidgetBuilderBase(TViewComponent component)
            : base(async delegate
            {
                await Task.Yield();
            })
        {
            Component = component;
        }

        public static implicit operator TViewComponent(WidgetBuilderBase<TViewComponent, TBuilder> builder)
        {
            return builder.ToComponent();
        }

        //
        // Summary:
        //     Returns the internal view component.
        public TViewComponent ToComponent()
        {
            return Component;
        }

        //
        // Summary:
        //     Sets the name of the component.
        //
        // Parameters:
        //   componentName:
        //     The name.
        public virtual TBuilder Expression(string modelExpression)
        {
            Component.Name = modelExpression;
            HasModelExpression = true;
            return this as TBuilder;
        }

        //
        // Summary:
        //     Sets the name of the component.
        //
        // Parameters:
        //   componentName:
        //     The name.
        public virtual TBuilder Explorer(ModelExplorer modelExplorer)
        {
            Component.Explorer = modelExplorer;
            return this as TBuilder;
        }

        //
        // Summary:
        //     Sets the name of the component.
        //
        // Parameters:
        //   componentName:
        //     The name.
        public virtual TBuilder Name(string componentName)
        {
            if (HasModelExpression)
            {
                throw new InvalidOperationException(Exceptions.YouCannotOverrideModelExpressionName);
            }

            Component.Name = componentName;
            return this as TBuilder;
        }

        //
        // Summary:
        //     Suppress initialization script rendering. Note that this options should be used
        //     in conjunction with WidgetFactory.DeferredScripts
        public virtual DeferredWidgetBuilder<TViewComponent> Deferred(bool deferred = true)
        {
            Component.HasDeferredInitialization = deferred;
            if (Component.HasDeferredInitialization)
            {
                Component.ToHtmlString();
                Component.WriteDeferredScriptInitialization();
            }

            return new DeferredWidgetBuilder<TViewComponent>(Component);
        }

        //
        // Summary:
        //     Sets the HTML attributes.
        //
        // Parameters:
        //   attributes:
        //     The HTML attributes.
        public virtual TBuilder HtmlAttributes(object attributes)
        {
            IDictionary<string, object> dictionary = null;
            if (attributes != null)
            {
                dictionary = attributes as IDictionary<string, object>;
                if (dictionary == null)
                {
                    dictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
                }
            }

            Component.HtmlAttributes = dictionary;
            return this as TBuilder;
        }

        //
        // Summary:
        //     Sets the HTML attributes.
        //
        // Parameters:
        //   attributes:
        //     The HTML attributes.
        public virtual TBuilder HtmlAttributes(IDictionary<string, object> attributes)
        {
            Component.HtmlAttributes = attributes;
            return this as TBuilder;
        }

        //
        // Summary:
        //     Sets the JavaScript attributes to the initialization script.
        //
        // Parameters:
        //   attributes:
        //     The JavaScript attributes.
        //
        //   overrideAttributes:
        //     Argument which determines whether attributes should be overriden.
        public virtual TBuilder ScriptAttributes(object attributes, bool overrideAttributes = false)
        {
            return ScriptAttributes(attributes.ToDictionary(), overrideAttributes);
        }

        //
        // Summary:
        //     Sets the JavaScript attributes to the initialization script.
        //
        // Parameters:
        //   attributes:
        //     The JavaScript attributes.
        //
        //   overrideAttributes:
        //     Argument which determines whether attributes should be overriden.
        public virtual TBuilder ScriptAttributes(IDictionary<string, object> attributes, bool overrideAttributes)
        {
            if (overrideAttributes)
            {
                Component.ScriptAttributes.Clear();
            }

            Component.ScriptAttributes.Merge(attributes, overrideAttributes);
            return this as TBuilder;
        }

        //
        // Summary:
        //     Renders the component in place.
        public virtual void Render()
        {
            Component.Render();
        }

        public virtual string ToHtmlString()
        {
            return ToComponent().ToHtmlString();
        }

        public override void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write(ToHtmlString());
        }

        public virtual HtmlString ToClientTemplate()
        {
            return ToComponent().ToClientTemplate();
        }

        //
        // Summary:
        //     Specifies whether the initialization script of the component will be rendered
        //     as a JavaScript module.
        public virtual TBuilder AsModule(bool value)
        {
            string value2 = (value ? "module" : null);
            Component.ScriptAttributes["type"] = value2;
            return this as TBuilder;
        }
    }
}
