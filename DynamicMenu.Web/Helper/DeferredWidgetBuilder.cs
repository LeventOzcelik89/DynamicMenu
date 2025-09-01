using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Text.Encodings.Web;

namespace DynamicMenu.Web.Helper
{
    public class DeferredWidgetBuilder<TViewComponent> : HelperResult where TViewComponent : WidgetBase
    {
        //
        // Summary:
        //     Gets the view component.
        //
        // Value:
        //     The component.
        protected internal TViewComponent Component { get; set; }

        public DeferredWidgetBuilder(TViewComponent component)
            : base(async delegate
            {
                await Task.Yield();
            })
        {
            Component = component;
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
    }
}
