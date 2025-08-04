using System.Globalization;

namespace DynamicMenu.Web.Helper
{
    public class AggregateResult
    {
        private object aggregateValue;

        private int itemCount;

        private readonly AggregateFunction function;

        //
        // Summary:
        //     Gets or sets the value of the result.
        //
        // Value:
        //     The value of the result.
        public object Value
        {
            get
            {
                return aggregateValue;
            }
            internal set
            {
                aggregateValue = value;
            }
        }

        public string Member => function.SourceField;

        //
        // Summary:
        //     Gets the formatted value of the result.
        //
        // Value:
        //     The formatted value of the result.
        public object FormattedValue
        {
            get
            {
                if (string.IsNullOrEmpty(function.ResultFormatString))
                {
                    return aggregateValue;
                }

                return string.Format(CultureInfo.CurrentCulture, function.ResultFormatString, aggregateValue);
            }
        }

        //
        // Summary:
        //     Gets or sets the number of arguments used for the calulation of the result.
        //
        // Value:
        //     The number of arguments used for the calulation of the result.
        public int ItemCount
        {
            get
            {
                return itemCount;
            }
            set
            {
                itemCount = value;
            }
        }

        //
        // Summary:
        //     Gets or sets the text which serves as a caption for the result in a user interface..
        //
        //
        // Value:
        //     The text which serves as a caption for the result in a user interface.
        public string Caption => function.Caption;

        //
        // Summary:
        //     Gets the name of the function.
        //
        // Value:
        //     The name of the function.
        public string FunctionName => function.FunctionName;

        public string AggregateMethodName => function.AggregateMethodName;

        //
        // Summary:
        //     Initializes a new instance of the Kendo.Mvc.Infrastructure.AggregateResult class.
        //
        //
        // Parameters:
        //   value:
        //     The value of the result.
        //
        //   count:
        //     The number of arguments used for the calculation of the result.
        //
        //   function:
        //     Function that generated the result.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     function is null.
        public AggregateResult(object value, int count, AggregateFunction function)
        {
            if (function == null)
            {
                throw new ArgumentNullException("function");
            }

            aggregateValue = value;
            itemCount = count;
            this.function = function;
        }

        //
        // Summary:
        //     Initializes a new instance of the Kendo.Mvc.Infrastructure.AggregateResult class.
        //
        //
        // Parameters:
        //   function:
        //     Kendo.Mvc.AggregateFunction that generated the result.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     function is null.
        public AggregateResult(AggregateFunction function)
            : this(null, function)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the Kendo.Mvc.Infrastructure.AggregateResult class.
        //
        //
        // Parameters:
        //   value:
        //     The value of the result.
        //
        //   function:
        //     Kendo.Mvc.AggregateFunction that generated the result.
        public AggregateResult(object value, AggregateFunction function)
            : this(value, 0, function)
        {
        }

        //
        // Summary:
        //     Returns a System.String that represents the current System.Object.
        //
        // Returns:
        //     A System.String that represents the current System.Object.
        public override string ToString()
        {
            if (Value != null)
            {
                return Value.ToString();
            }

            return base.ToString();
        }

        public string Format(string format)
        {
            if (Value != null)
            {
                return string.Format(format, Value);
            }

            return ToString();
        }
    }
}
