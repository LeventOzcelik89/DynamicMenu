using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Net.Mime.MediaTypeNames;

namespace DynamicMenu.Web.UIHelper
{
    public static class MTextBox : FactoryBase
    {

        public static MTextBoxBuilder MTextBox(HtmlHelper helper)
        {

        //  <div class="mb-3">
        //      <label for="TextEn" class="form-label">Text En</label>
        //      @Html.TextBoxFor(a => a.TextEn, null, new Dictionary<string, object> { { "class", "form-control" }, { "required", "required" } })
        //      <div class="invalid-feedback">Boş bırakılamaz</div>
        //  </div>

            return helper.TextBox().NumericTextBox().
                HtmlAttributes(new Dictionary<string, object>()
                {
                    {"style", "width:100%"},
                })
                .Step(1)
                .Spinners(false)
                .Culture(Extensions.Culture().Name);
        }

    }
}
