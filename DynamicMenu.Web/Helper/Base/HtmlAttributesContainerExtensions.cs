namespace DynamicMenu.Web.Helper
{
    public static class HtmlAttributesContainerExtensions
    {
        public static void ThrowIfClassIsPresent(this IHtmlAttributesContainer container, string @class, string message)
        {
            if (container.HtmlAttributes.TryGetValue("class", out var value) && value != null && Array.IndexOf(value.ToString().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries), @class) > -1)
            {
                throw new NotSupportedException(message.FormatWith(@class));
            }
        }
    }
}
