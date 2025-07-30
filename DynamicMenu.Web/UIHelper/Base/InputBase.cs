using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DynamicMenu.Web.UIHelper
{
    public class InputBase : FactoryBase
    {
        public InputBase(ViewContext viewContext, ViewDataDictionary viewData = null) : base(viewContext, viewData)
        {
        }

        public string Template { get; set; }
        public string TemplateId { get; set; }
        public string Value { get; set; }

        protected virtual IDictionary<string, object> SeriailzeBaseOptions()
        {
            return null;
        }

    }
}
