using System.Collections.ObjectModel;

namespace DynamicMenu.Web.Helper
{
    public class AggregateFunctionCollection : Collection<AggregateFunction>
    {
        //
        // Summary:
        //     Gets the Kendo.Mvc.AggregateFunction with the specified function name.
        //
        // Value:
        //     First Kendo.Mvc.AggregateFunction with the specified function name if any, otherwise
        //     null.
        public AggregateFunction this[string functionName] => this.FirstOrDefault((AggregateFunction f) => f.FunctionName == functionName);
    }
}
