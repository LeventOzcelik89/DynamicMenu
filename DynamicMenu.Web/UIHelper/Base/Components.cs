using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DynamicMenu.Web.UIHelper
{
    public static class HtmlHelperExtensions
    {
        public static Factory<TModel> HalkBank<TModel>(this IHtmlHelper<TModel> helper)
        {
            return new Factory<TModel>(helper);
        }

        public static Factory HalkBank(this HtmlHelper helper)
        {
            return new Factory(helper);
        }

        //public static string HalkBank(this IHtmlHelper htmlHelper)
        //{
        //    // Your implementation
        //    return "Akilli!";
        //}

    }
}