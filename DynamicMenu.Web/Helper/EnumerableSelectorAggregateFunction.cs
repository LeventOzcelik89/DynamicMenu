using System.Linq.Expressions;

namespace DynamicMenu.Web.Helper
{
    public abstract class EnumerableSelectorAggregateFunction : EnumerableAggregateFunctionBase
    {
        //
        // Summary:
        //     Creates the aggregate expression using Kendo.Mvc.Infrastructure.Implementation.Expressions.EnumerableSelectorAggregateFunctionExpressionBuilder.
        //
        //
        // Parameters:
        //   enumerableExpression:
        //     The grouping expression.
        //
        //   liftMemberAccessToNull:
        public override Expression CreateAggregateExpression(Expression enumerableExpression, bool liftMemberAccessToNull)
        {
            EnumerableSelectorAggregateFunctionExpressionBuilder enumerableSelectorAggregateFunctionExpressionBuilder = new EnumerableSelectorAggregateFunctionExpressionBuilder(enumerableExpression, this);
            enumerableSelectorAggregateFunctionExpressionBuilder.Options.LiftMemberAccessToNull = liftMemberAccessToNull;
            return enumerableSelectorAggregateFunctionExpressionBuilder.CreateAggregateExpression();
        }
    }
}
