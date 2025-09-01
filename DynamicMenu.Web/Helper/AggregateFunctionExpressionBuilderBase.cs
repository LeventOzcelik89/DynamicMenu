using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicMenu.Web.Helper
{
    internal abstract class AggregateFunctionExpressionBuilderBase : ExpressionBuilderBase
    {
        private readonly AggregateFunction function;

        private readonly Expression enumerableExpression;

        protected AggregateFunction Function => function;

        protected Expression EnumerableExpression => enumerableExpression;

        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     Provided enumerableExpression's System.Linq.Expressions.Expression.Type is not
        //     System.Collections.Generic.IEnumerable`1
        protected AggregateFunctionExpressionBuilderBase(Expression enumerableExpression, AggregateFunction function)
            : base(ExtractItemTypeFromEnumerableType(enumerableExpression.Type))
        {
            this.enumerableExpression = enumerableExpression;
            this.function = function;
        }

        public abstract Expression CreateAggregateExpression();

        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     Provided type is not System.Collections.Generic.IEnumerable`1
        private static Type ExtractItemTypeFromEnumerableType(Type type)
        {
            Type type2 = type.GetGenericTypeDefinition();
            if (type2 == null)
            {
                throw new ArgumentException("Provided type is not IEnumerable<>", "type");
            }

            return type2.GenericTypeArguments.First();
        }
    }
}
