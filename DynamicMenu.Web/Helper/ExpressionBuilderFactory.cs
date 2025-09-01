using System;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Xml;

namespace DynamicMenu.Web.Helper
{
    public static class ExpressionBuilderFactory
    {
        public static MemberAccessExpressionBuilderBase MemberAccess(Type elementType, Type memberType, string memberName)
        {
            memberType = memberType ?? typeof(object);
            if (elementType.IsCompatibleWith(typeof(XmlNode)))
            {
                return new XmlNodeChildElementAccessExpressionBuilder(memberName);
            }

            if (elementType.IsCompatibleWith(typeof(ICustomTypeDescriptor)))
            {
                return new CustomTypeDescriptorPropertyAccessExpressionBuilder(elementType, memberType, memberName);
            }

            if (elementType == typeof(object) || elementType.IsCompatibleWith(typeof(IDynamicMetaObjectProvider)))
            {
                return new DynamicPropertyAccessExpressionBuilder(elementType, memberName);
            }

            return new PropertyAccessExpressionBuilder(elementType, memberName);
        }

        public static MemberAccessExpressionBuilderBase MemberAccess(Type elementType, string memberName, bool liftMemberAccess)
        {
            MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = MemberAccess(elementType, null, memberName);
            memberAccessExpressionBuilderBase.Options.LiftMemberAccessToNull = liftMemberAccess;
            return memberAccessExpressionBuilderBase;
        }

        public static MemberAccessExpressionBuilderBase MemberAccess(Type elementType, Type memberType, string memberName, bool liftMemberAccess)
        {
            MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = MemberAccess(elementType, memberType, memberName);
            memberAccessExpressionBuilderBase.Options.LiftMemberAccessToNull = liftMemberAccess;
            return memberAccessExpressionBuilderBase;
        }

        public static MemberAccessExpressionBuilderBase MemberAccess(IQueryable source, Type memberType, string memberName)
        {
            MemberAccessExpressionBuilderBase memberAccessExpressionBuilderBase = MemberAccess(source.ElementType, memberType, memberName);
            memberAccessExpressionBuilderBase.Options.LiftMemberAccessToNull = source.Provider.IsLinqToObjectsProvider();
            return memberAccessExpressionBuilderBase;
        }
    }
}
