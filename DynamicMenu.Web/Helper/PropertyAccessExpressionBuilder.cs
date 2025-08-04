using System.Linq.Expressions;

namespace DynamicMenu.Web.Helper
{
    internal class PropertyAccessExpressionBuilder : MemberAccessExpressionBuilderBase
    {
        public PropertyAccessExpressionBuilder(Type itemType, string memberName)
            : base(itemType, memberName)
        {
        }

        public override Expression CreateMemberAccessExpression()
        {
            if (string.IsNullOrEmpty(base.MemberName))
            {
                return base.ParameterExpression;
            }

            return ExpressionFactory.MakeMemberAccess(base.ParameterExpression, base.MemberName, base.Options.LiftMemberAccessToNull);
        }
    }
}
