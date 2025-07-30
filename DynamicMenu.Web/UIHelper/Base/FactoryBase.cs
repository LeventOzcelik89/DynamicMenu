using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DynamicMenu.Web.UIHelper
{
    public class FactoryBase : IFactory, IScriptable
    {

        protected FactoryBase(ViewContext viewContext, ViewDataDictionary viewData = null)
        {
            this.ViewData = viewData;
            this.ViewContext = viewContext;
        }

        public ModelMetadata ModelMetadata { get; set; }

        public ViewContext ViewContext { get; set; }

        public ViewDataDictionary ViewData { get; set; }

        public void WriteInitializationScript(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        protected virtual void WriteHtml(TextWriter writer)
        {

        }

    }
}
