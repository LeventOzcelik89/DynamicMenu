using System.ComponentModel;
using System.Globalization;

namespace DynamicMenu.Web.Helper
{
    internal static class CustomTypeDescriptorExtensions
    {
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     ArgumentException.
        public static T Property<T>(this ICustomTypeDescriptor typeDescriptor, string propertyName)
        {
            PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeDescriptor)[propertyName];
            if (propertyDescriptor == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Property with specified name: {0} cannot be found on type: {1}", propertyName, typeDescriptor.GetType()), "propertyName");
            }

            return UnboxT<T>.Unbox(propertyDescriptor.GetValue(typeDescriptor));
        }
    }
}
