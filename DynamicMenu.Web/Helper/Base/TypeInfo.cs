using System.Reflection;

namespace DynamicMenu.Web.Helper
{
    internal class TypeInfo
    {
        public string NS { get; set; }

        public string AssemblyName { get; set; }

        public string TypeName { get; set; }

        internal static Type GetExistingType(IEnumerable<TypeInfo> typesInfo)
        {
            Type type = null;
            foreach (TypeInfo item in typesInfo)
            {
                type = GetType(item);
                if (type != null)
                {
                    return type;
                }
            }

            return type;
        }

        internal static object InvokeGenericMethod(object instance, string methodName, Type[] typeParameters, object[] parameters = null)
        {
            return GetMethod(instance.GetType(), methodName, parameters).MakeGenericMethod(typeParameters).Invoke(instance, parameters);
        }

        internal static object InvokeExtensionMethod(Type type, object instance, string methodName, Type[] typeParameters = null, object[] parameters = null)
        {
            List<object> list = new List<object> { instance };
            if (parameters != null)
            {
                list.AddRange(parameters);
            }

            MethodInfo methodInfo = GetMethod(type, methodName, list.ToArray());
            if (typeParameters != null)
            {
                methodInfo = methodInfo.MakeGenericMethod(typeParameters);
            }

            return methodInfo.Invoke(null, list.ToArray());
        }

        internal static Type GetType(TypeInfo typeInfo)
        {
            return Type.GetType($"{typeInfo.NS}.{typeInfo.TypeName}, {typeInfo.AssemblyName}");
        }

        internal static MethodInfo GetMethod(Type type, string methodName, object[] parameters = null)
        {
            Type[] types = ((parameters == null) ? new Type[0] : parameters.Select((object t) => t.GetType()).ToArray());
            return type.GetTypeInfo().GetMethod(methodName, types);
        }
    }
}
