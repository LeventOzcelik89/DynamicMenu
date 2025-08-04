using System.Linq.Expressions;

namespace DynamicMenu.Web.Helper
{
    public abstract class MemberAccessExpressionBuilderBase : ExpressionBuilderBase
    {
        private readonly string memberName;

        public string MemberName => memberName;

        protected MemberAccessExpressionBuilderBase(Type itemType, string memberName)
            : base(itemType)
        {
            this.memberName = memberName;
        }

        public abstract Expression CreateMemberAccessExpression();

        public LambdaExpression CreateLambdaExpression()
        {
            return Expression.Lambda(CreateMemberAccessExpression(), base.ParameterExpression);
        }
    }
}
