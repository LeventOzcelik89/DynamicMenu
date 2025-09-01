using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DynamicMenu.Web.UIHelper
{
    public class Factory : IHideMembers
    {
        public HtmlHelper HtmlHelper { get; set; }

        public Factory(HtmlHelper htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

        public virtual MTextBoxBuilder MTextBox()
        {
            var comp = new MTextBox(this.HtmlHelper.ViewContext, this.HtmlHelper.ViewData);
            return new MTextBoxBuilder(comp);
        }

    }

    public class Factory<TModel> : IHideMembers
    {

        public IHtmlHelper<TModel> HtmlHelper { get; set; }

        public Factory(IHtmlHelper<TModel> htmlHelper)
        {
            this.HtmlHelper = htmlHelper;
        }

        public virtual MTextBoxBuilder MTextBox()
        {

            var comp = new MTextBox(this.HtmlHelper.ViewContext, this.HtmlHelper.ViewData);
            return new MTextBoxBuilder(comp);

        }

    }

}
