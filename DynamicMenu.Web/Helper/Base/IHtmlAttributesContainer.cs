namespace DynamicMenu.Web.Helper
{
    public interface IHtmlAttributesContainer
    {
        //
        // Summary:
        //     The HtmlAttributes applied to objects which can have child items
        IDictionary<string, object> HtmlAttributes { get; }
    }
}
