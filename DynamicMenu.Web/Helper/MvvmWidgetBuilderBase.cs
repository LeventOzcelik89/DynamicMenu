namespace DynamicMenu.Web.Helper
{
    public class MvvmWidgetBuilderBase<TViewComponent, TBuilder> : WidgetBuilderBase<TViewComponent, TBuilder> where TViewComponent : WidgetBase where TBuilder : WidgetBuilderBase<TViewComponent, TBuilder>
    {
        public MvvmWidgetBuilderBase(TViewComponent component)
            : base(component)
        {
        }

        //
        // Summary:
        //     Specifies if the component should be initialized through MVVM on the client.
        //
        //
        // Parameters:
        //   value:
        //     The value.
        public virtual TBuilder UseMvvmInitialization(bool value)
        {
            base.Component.UseMvvmInitialization = value;
            return this as TBuilder;
        }

        //
        // Summary:
        //     Specifies if the component should be initialized through MVVM on the client.
        public virtual TBuilder UseMvvmInitialization()
        {
            base.Component.UseMvvmInitialization = true;
            return this as TBuilder;
        }
    }
}
