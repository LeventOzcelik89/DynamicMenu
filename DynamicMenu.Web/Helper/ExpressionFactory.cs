using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace DynamicMenu.Web.Helper
{
    public static class ExpressionFactory
    {
        public static ConstantExpression ZeroExpression => Expression.Constant(0);

        public static ConstantExpression EmptyStringExpression => Expression.Constant(string.Empty);

        public static Expression DefaltValueExpression(Type type)
        {
            return Expression.Constant(type.GetDefaultValue(), type);
        }

        public static Expression MakeMemberAccess(Expression instance, string memberName)
        {
            foreach (IMemberAccessToken token in MemberAccessTokenizer.GetTokens(memberName))
            {
                instance = token.CreateMemberAccessExpression(instance);
            }

            return instance;
        }

        public static Expression MakeMemberAccess(Expression instance, string memberName, bool liftMemberAccessToNull)
        {
            Expression expression = MakeMemberAccess(instance, memberName);
            if (liftMemberAccessToNull)
            {
                return LiftMemberAccessToNull(expression);
            }

            return expression;
        }

        public static Expression LiftMemberAccessToNull(Expression memberAccess)
        {
            Expression defaultValue = DefaltValueExpression(memberAccess.Type);
            return LiftMemberAccessToNullRecursive(memberAccess, memberAccess, defaultValue);
        }

        public static Expression LiftMethodCallToNull(Expression instance, MethodInfo method, params Expression[] arguments)
        {
            return LiftMemberAccessToNull(Expression.Call(ExtractMemberAccessExpressionFromLiftedExpression(instance), method, arguments));
        }

        private static Expression LiftMemberAccessToNullRecursive(Expression memberAccess, Expression conditionalExpression, Expression defaultValue)
        {
            Expression instanceExpressionFromExpression = GetInstanceExpressionFromExpression(memberAccess);
            if (instanceExpressionFromExpression == null)
            {
                return conditionalExpression;
            }

            conditionalExpression = CreateIfNullExpression(instanceExpressionFromExpression, conditionalExpression, defaultValue);
            return LiftMemberAccessToNullRecursive(instanceExpressionFromExpression, conditionalExpression, defaultValue);
        }

        private static Expression GetInstanceExpressionFromExpression(Expression memberAccess)
        {
            if (memberAccess is MemberExpression memberExpression)
            {
                return memberExpression.Expression;
            }

            if (memberAccess is MethodCallExpression methodCallExpression)
            {
                return methodCallExpression.Object;
            }

            return null;
        }

        private static Expression CreateIfNullExpression(Expression instance, Expression memberAccess, Expression defaultValue)
        {
            if (ShouldGenerateCondition(instance.Type))
            {
                return CreateConditionExpression(instance, memberAccess, defaultValue);
            }

            return memberAccess;
        }

        private static bool ShouldGenerateCondition(Type type)
        {
            if (type.IsValueType())
            {
                return type.IsNullableType();
            }

            return true;
        }

        private static Expression CreateConditionExpression(Expression instance, Expression memberAccess, Expression defaultValue)
        {
            Expression right = DefaltValueExpression(instance.Type);
            return Expression.Condition(Expression.NotEqual(instance, right), memberAccess, defaultValue);
        }

        private static Expression ExtractMemberAccessExpressionFromLiftedExpression(Expression liftedToNullExpression)
        {
            while (liftedToNullExpression.NodeType == ExpressionType.Conditional)
            {
                ConditionalExpression conditionalExpression = (ConditionalExpression)liftedToNullExpression;
                liftedToNullExpression = ((conditionalExpression.Test.NodeType != ExpressionType.NotEqual) ? conditionalExpression.IfFalse : conditionalExpression.IfTrue);
            }

            return liftedToNullExpression;
        }

        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     Provided expression should have string type
        internal static Expression LiftStringExpressionToEmpty(Expression stringExpression)
        {
            if (stringExpression.Type != typeof(string))
            {
                throw new ArgumentException("Provided expression should have string type", "stringExpression");
            }

            if (IsNotNullConstantExpression(stringExpression))
            {
                return stringExpression;
            }

            return Expression.Coalesce(stringExpression, EmptyStringExpression);
        }

        internal static bool IsNotNullConstantExpression(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
            {
                return ((ConstantExpression)expression).Value != null;
            }

            return false;
        }
    }
}
