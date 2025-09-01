using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace DynamicMenu.Web.Helper
{
    public class WidgetFactory<TModel>
    {
        public delegate void WidgetCreatedDelegate(WidgetBase widget);

        private const string MinimumValidator = "min";

        private const string MaximumValidator = "max";

        private const string Script = "script";

        private const string Style = "style";

        private readonly IUrlHelper urlHelper;

        private readonly IKendoOptions kendoOptions;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IHtmlHelper<TModel> HtmlHelper { get; set; }

        public event WidgetCreatedDelegate WidgetCreated;

        //  Burayı yapacağım. Datasource vs çok fazla geldi.
        public virtual MTextBoxBuilder<T> MTextBox<T>() where T : struct
        {
            MTextBox<T> mTextBox = new MTextBox<T>(HtmlHelper.ViewContext);
            this.WidgetCreated?.Invoke(mTextBox);
            return new MTextBoxBuilder<T>(mTextBox);
        }



        //
        // Summary:
        //     Creates a new Kendo.Mvc.UI.MultiSelect.
        public virtual MTextBoxBuilder MTextBox()
        {
            MTextBox mTextBox = new MTextBox(HtmlHelper.ViewContext);
            this.WidgetCreated?.Invoke(mTextBox);
            return new MultiSelectBuilder(mTextBox);
        }

    }
}
