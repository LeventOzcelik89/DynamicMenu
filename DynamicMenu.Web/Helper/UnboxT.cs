using System.Globalization;
using System.Reflection;

namespace DynamicMenu.Web.Helper
{
    internal static class UnboxT<T>
    {
        internal static readonly Func<object, T> Unbox = Create(typeof(T));

        private static Func<object, T> Create(Type type)
        {
            if (!type.IsValueType)
            {
                return ReferenceField;
            }

            if (type.IsGenericType && !type.GetTypeInfo().IsGenericTypeDefinition && typeof(Nullable<>) == type.GetGenericTypeDefinition())
            {
                return (Func<object, T>)typeof(UnboxT<T>).GetMethod("NullableField", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(type.GetGenericArguments()[0]).CreateDelegate(typeof(Func<object, T>));
            }

            return ValueField;
        }

        private static TElem? NullableField<TElem>(object value) where TElem : struct
        {
            if (DBNull.Value == value)
            {
                return null;
            }

            return (TElem?)value;
        }

        private static T ReferenceField(object value)
        {
            if (DBNull.Value != value)
            {
                return (T)value;
            }

            return default(T);
        }

        //
        // Exceptions:
        //   T:System.InvalidCastException:
        //     InvalidCastException.
        private static T ValueField(object value)
        {
            if (DBNull.Value == value)
            {
                throw new InvalidCastException(string.Format(CultureInfo.CurrentCulture, "Type: {0} cannot be casted to Nullable type", typeof(T)));
            }

            return (T)value;
        }
    }
}
