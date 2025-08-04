using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.CSharp.RuntimeBinder;

namespace DynamicMenu.Web.Helper
{
    public class DynamicPropertyAccessExpressionBuilder : MemberAccessExpressionBuilderBase
    {
        public DynamicPropertyAccessExpressionBuilder(Type itemType, string memberName)
            : base(itemType, memberName)
        {
        }

        public override Expression CreateMemberAccessExpression()
        {
            if (string.IsNullOrEmpty(base.MemberName))
            {
                return base.ParameterExpression;
            }

            Expression expression = base.ParameterExpression;
            foreach (IMemberAccessToken token in MemberAccessTokenizer.GetTokens(base.MemberName))
            {
                if (token is PropertyToken)
                {
                    string propertyName = ((PropertyToken)token).PropertyName;
                    expression = CreatePropertyAccessExpression(expression, propertyName);
                }
                else if (token is IndexerToken)
                {
                    expression = CreateIndexerAccessExpression(expression, (IndexerToken)token);
                }
            }

            return expression;
        }

        private Expression CreateIndexerAccessExpression(Expression instance, IndexerToken indexerToken)
        {
            return DynamicExpression.Dynamic(Binder.GetIndex(CSharpBinderFlags.None, typeof(DynamicPropertyAccessExpressionBuilder), new CSharpArgumentInfo[2]
            {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
            }), typeof(object), new Expression[2]
            {
            instance,
            indexerToken.Arguments.Select(Expression.Constant).First()
            });
        }

        private Expression CreatePropertyAccessExpression(Expression instance, string propertyName)
        {
            return DynamicExpression.Dynamic(Binder.GetMember(CSharpBinderFlags.None, propertyName, typeof(DynamicPropertyAccessExpressionBuilder), new CSharpArgumentInfo[1] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }), typeof(object), new Expression[1] { instance });
        }
    }
}
