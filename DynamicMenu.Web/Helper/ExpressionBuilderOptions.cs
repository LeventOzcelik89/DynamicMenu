namespace DynamicMenu.Web.Helper
{
    public class ExpressionBuilderOptions
    {
        //
        // Summary:
        //     Gets or sets a value indicating whether member access expression used by this
        //     builder should be lifted to null. The default value is true;
        //
        // Value:
        //     true if member access should be lifted to null; otherwise, false.
        public bool LiftMemberAccessToNull { get; set; }

        public ExpressionBuilderOptions()
        {
            LiftMemberAccessToNull = true;
        }

        public void CopyFrom(ExpressionBuilderOptions other)
        {
            LiftMemberAccessToNull = other.LiftMemberAccessToNull;
        }
    }
}
