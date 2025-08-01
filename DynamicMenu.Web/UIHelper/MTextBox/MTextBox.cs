using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;
namespace DynamicMenu.Web.UIHelper
{
    public class MTextBox : FactoryBase
    {
        public IDictionary<string, object> htmlAttributes { get; set; }
        public IDictionary<string, object> containerHtmlAttributes { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string value { get; set; }
        public string label { get; set; }
        public string type { get; set; } = "text"; // Default type is text
        public Validation validation { get; set; }
        public bool readOnly { get; set; } = false;

        public MTextBox(ViewContext viewContext, ViewDataDictionary viewData = null) : base(viewContext, viewData)
        {
            this.htmlAttributes = new Dictionary<string, object>();
        }
        
        public string Render()
        {

            //  <div class="mb-3">
            //      <label for="TextEn" class="form-label">Text En</label>
            //      @Html.TextBoxFor(a => a.TextEn, null, new Dictionary<string, object> { { "class", "form-control" }, { "required", "required" } })
            //      <div class="invalid-feedback">Boş bırakılamaz</div>
            //  </div>

            var container = new TagBuilder("div");
            foreach (var attr in this.containerHtmlAttributes)
            {
                container.Attributes.Add(attr.Key, attr.Value.ToString());
            }

            var label = new TagBuilder("label");
            label.Attributes.Add("for", this.id);
            label.Attributes.Add("class", "form-label");
            label.InnerHtml.AppendHtml(this.label);

            //  ****
            container.InnerHtml.AppendHtml(label);

            var input = new TagBuilder("input");
            input.Attributes.Add("type", this.type);
            input.Attributes.Add("id", this.id);
            input.Attributes.Add("name", this.name);
            input.Attributes.Add("class", "form-control");
            input.Attributes.Add("value", this.value ?? string.Empty); // Ensure value is not null

            if (this.validation != null && this.validation.required)
            {
                input.Attributes.Add("required", "required");
            }

            //  ****
            container.InnerHtml.AppendHtml(input);

            if (!string.IsNullOrEmpty(validation?.invalidFeedback))
            {
                var invalidValidation = new TagBuilder("div");
                invalidValidation.Attributes.Add("class", "invalid-feedback");
                invalidValidation.InnerHtml.AppendHtml(validation.invalidFeedback);
                container.InnerHtml.AppendHtml(invalidValidation);
            }

            if (!string.IsNullOrEmpty(validation?.validFeedback))
            {
                var validValidation = new TagBuilder("div");
                validValidation.Attributes.Add("class", "valid-feedback");
                validValidation.InnerHtml.AppendHtml(validation.validFeedback);
                container.InnerHtml.AppendHtml(validValidation);
            }

            return container.ToString();


            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"mb-3\">");
            sb.AppendLine("<label for=\"TextEn\" class=\"form-label\">Text En</label>");
            sb.AppendLine("<input type=\"text\" id=\"TextEn\" name=\"TextEn\" class=\"form-control\" required />");
            sb.AppendLine("<div class=\"invalid-feedback\">Boş bırakılamaz</div>");
            sb.AppendLine("</div>");

            return sb.ToString();

        }

    }
}
