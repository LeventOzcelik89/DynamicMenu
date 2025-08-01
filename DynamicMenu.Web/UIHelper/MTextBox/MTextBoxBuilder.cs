using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace DynamicMenu.Web.UIHelper
{
    public class MTextBoxBuilder : FactoryBuilderBase<MTextBox, MTextBoxBuilder>
    {
        public MTextBoxBuilder(MTextBox component) : base(component) { }

        public MTextBoxBuilder Name(string name)
        {
            this.Component.id = name;
            this.Component.name = name;
            return this;
        }

        public MTextBoxBuilder HtmlAttributes(Dictionary<string, object> attributes)
        {
            this.Component.htmlAttributes = attributes;
            return this;
        }

        public MTextBoxBuilder ReadOnly(bool readOnly)
        {
            this.Component.readOnly = readOnly;
            return this;
        }

        public MTextBoxBuilder Value(string value)
        {
            this.Component.value = value;
            return this;
        }

        public MTextBoxBuilder Label(string label)
        {
            this.Component.label = label;
            return this;
        }

        public MTextBoxBuilder Type(string type)
        {
            this.Component.type = type;
            return this;
        }

        public MTextBoxBuilder Validation(Validation validation)
        {
            this.Component.validation = validation;
            return this;
        }

    }
}
