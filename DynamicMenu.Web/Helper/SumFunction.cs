namespace DynamicMenu.Web.Helper
{
    public class SumFunction : EnumerableSelectorAggregateFunction
    {
        //
        // Summary:
        //     Gets the the Min method name.
        //
        // Value:
        //     Min.
        public override string AggregateMethodName => "Sum";

        //
        // Summary:
        //     Initializes a new instance of the Kendo.Mvc.SumFunction class.
        public SumFunction()
        {
        }
    }
}
