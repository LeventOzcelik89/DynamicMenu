using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DynamicMenu.Web.UIHelper
{
    public interface IFactory
    {
        ModelMetadata ModelMetadata { get; }
        ViewContext ViewContext { get; }
        ViewDataDictionary ViewData { get; }
    }
}
