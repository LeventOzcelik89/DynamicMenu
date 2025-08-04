using System.Globalization;

namespace DynamicMenu.Web.Helper
{
    public abstract class EnumerableAggregateFunctionBase : AggregateFunction
    {
        //
        // Summary:
        //     Gets the type of the extension methods that holds the extension methods for aggregation.
        //     For example System.Linq.Enumerable or System.Linq.Queryable.
        //
        // Value:
        //     The type of that holds the extension methods. The default value is System.Linq.Enumerable.
        protected internal virtual Type ExtensionMethodsType => typeof(Enumerable);

        protected override string GenerateFunctionName()
        {
            string text = SourceField;
            if (text.HasValue())
            {
                text = text.Replace(".", "-");
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", AggregateMethodName, text);
        }
    }
}
