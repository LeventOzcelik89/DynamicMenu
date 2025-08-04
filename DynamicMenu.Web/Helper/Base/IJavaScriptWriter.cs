namespace DynamicMenu.Web.Helper
{
    public interface IJavaScriptWriter
    {
        void WriteScriptAttributes(TextWriter writer, IDictionary<string, object> attributes);

        void WriteOpeningTag(TextWriter writer);

        void WriteClosingTag(TextWriter writer);
    }
}
