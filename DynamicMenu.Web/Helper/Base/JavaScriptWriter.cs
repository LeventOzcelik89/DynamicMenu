namespace DynamicMenu.Web.Helper
{
    public class JavaScriptWriter : IJavaScriptWriter
    {
        public void WriteOpeningTag(TextWriter writer)
        {
            writer.Write("<script>");
        }

        public void WriteScriptAttributes(TextWriter writer, IDictionary<string, object> attributes)
        {
            writer.Write("<script ");
            foreach (KeyValuePair<string, object> item in attributes.Where((KeyValuePair<string, object> attribute) => attribute.Value != null))
            {
                writer.Write($"{item.Key}=\"{item.Value}\" ");
            }

            writer.Write(">");
        }

        public void WriteClosingTag(TextWriter writer)
        {
            writer.Write("</script>");
        }
    }
}
