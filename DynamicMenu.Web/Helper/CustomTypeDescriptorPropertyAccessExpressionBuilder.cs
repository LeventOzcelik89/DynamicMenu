using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace DynamicMenu.Web.Helper
{
    internal class CustomTypeDescriptorPropertyAccessExpressionBuilder : MemberAccessExpressionBuilderBase
    {
        private static readonly MethodInfo PropertyMethod = typeof(CustomTypeDescriptorExtensions).GetMethod("Property");

        private readonly Type propertyType;

        public Type PropertyType => propertyType;

        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     elementType did not implement System.ComponentModel.ICustomTypeDescriptor.
        public CustomTypeDescriptorPropertyAccessExpressionBuilder(Type elementType, Type memberType, string memberName)
            : base(elementType, memberName)
        {
            if (!elementType.IsCompatibleWith(typeof(ICustomTypeDescriptor)))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ElementType: {0} did not implement {1}", elementType, typeof(ICustomTypeDescriptor)), "elementType");
            }

            propertyType = GetPropertyType(memberType);
        }

        private Type GetPropertyType(Type memberType)
        {
            Type propertyTypeFromTypeDescriptorProvider = GetPropertyTypeFromTypeDescriptorProvider();
            if (propertyTypeFromTypeDescriptorProvider != null)
            {
                memberType = propertyTypeFromTypeDescriptorProvider;
            }

            if (memberType.IsValueType && !memberType.IsNullableType())
            {
                return typeof(Nullable<>).MakeGenericType(memberType);
            }

            return memberType;
        }

        private Type GetPropertyTypeFromTypeDescriptorProvider()
        {
            return TypeDescriptor.GetProperties(base.ItemType)[base.MemberName]?.PropertyType;
        }

        public override Expression CreateMemberAccessExpression()
        {
            if (base.ParameterExpression.Type.IsAssignableFrom(typeof(DataRowView)))
            {
                ConstantExpression arg = Expression.Constant(base.MemberName);
                return Expression.Call(PropertyMethod.MakeGenericMethod(propertyType), base.ParameterExpression, arg);
            }

            return Expression.Property(base.ParameterExpression, base.MemberName);
        }
    }
}
