using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace DynamicMenu.Web.Helper
{
    internal class XmlNodeChildElementAccessExpressionBuilder : MemberAccessExpressionBuilderBase
    {
        private static readonly MethodInfo ChildElementInnerTextMethod = typeof(XmlNodeExtensions).GetMethod("ChildElementInnerText", new Type[2]
        {
        typeof(XmlNode),
        typeof(string)
        });

        public XmlNodeChildElementAccessExpressionBuilder(string memberName)
            : base(typeof(XmlNode), memberName)
        {
        }

        public override Expression CreateMemberAccessExpression()
        {
            ConstantExpression arg = Expression.Constant(base.MemberName);
            return Expression.Call(ChildElementInnerTextMethod, base.ParameterExpression, arg);
        }
    }
}
