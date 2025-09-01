using System.Globalization;
using System.Linq.Expressions;

namespace DynamicMenu.Web.Helper
{
    public abstract class AggregateFunction : JsonObject
    {
        private string functionName;

        public abstract string AggregateMethodName { get; }

        //
        // Summary:
        //     Gets or sets the informative message to display as an illustration of the aggregate
        //     function.
        //
        // Value:
        //     The caption to display as an illustration of the aggregate function.
        public string Caption { get; set; }

        //
        // Summary:
        //     Gets or sets the name of the field, of the item from the set of items, which
        //     value is used as the argument of the aggregate function.
        //
        // Value:
        //     The name of the field to get the argument value from.
        public virtual string SourceField { get; set; }

        //
        // Summary:
        //     Gets or sets the name of the aggregate function, which appears as a property
        //     of the group record on which records the function works.
        //
        // Value:
        //     The name of the function as visible from the group record.
        public virtual string FunctionName
        {
            get
            {
                if (string.IsNullOrEmpty(functionName))
                {
                    functionName = GenerateFunctionName();
                }

                return functionName;
            }
            set
            {
                functionName = value;
            }
        }

        public Type MemberType { get; set; }

        //
        // Summary:
        //     Gets or sets a string that is used to format the result value.
        //
        // Value:
        //     The format string.
        public virtual string ResultFormatString { get; set; }

        //
        // Summary:
        //     Creates the aggregate expression that is used for constructing expression tree
        //     that will calculate the aggregate result.
        //
        // Parameters:
        //   enumerableExpression:
        //     The grouping expression.
        //
        //   liftMemberAccessToNull:
        public abstract Expression CreateAggregateExpression(Expression enumerableExpression, bool liftMemberAccessToNull);

        //
        // Summary:
        //     Generates default name for this function using this type's name.
        //
        // Returns:
        //     Function name generated with the following pattern: {System.Object.GetType.System.Reflection.MemberInfo.Name}_{System.Object.GetHashCode}
        protected virtual string GenerateFunctionName()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", GetType().Name);
        }

        protected override void Serialize(IDictionary<string, object> json)
        {
            json["field"] = SourceField;
            json["aggregate"] = FunctionName.Split('_')[0].ToLowerInvariant();
        }
    }
}
