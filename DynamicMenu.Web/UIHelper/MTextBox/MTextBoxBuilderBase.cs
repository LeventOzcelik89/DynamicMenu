using Microsoft.AspNetCore.Components.Forms;

namespace DynamicMenu.Web.UIHelper
{
    public class MTextBoxBuilderBase<TMapInput, TMapInputBuilder> : InputBuilderBase<TMapInput, TMapInputBuilder>, IHideMembers
        where TMapInput : InputBase
        where TMapInputBuilder : FactoryBuilderBase<TMapInput, TMapInputBuilder>
    {
        public MTextBoxBuilderBase(TMapInput component) : base(component)
        {
            base.Component = component;
        }

        public TMapInputBuilder Template(string template)
        {
            return null;
        }

        public TMapInputBuilder TemplateId(string templateId)
        {
            return null;
        }

        public TMapInputBuilder Value(string value)
        {
            return null;
        }

    }
    
}
