using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Encodings.Web;

namespace DynamicMenu.Web.Helper
{
    public static class ViewContextExtensions
    {
        internal static readonly string DeferredScriptsKey = "$DeferredScriptsKey$";

        internal static readonly string DeferredStylesKey = "$DeferredStylesKey$";

        internal const string HtmlHelperLicensingScriptId = "kendo-html-helper-licensing-script-rendered";

        internal const string TagHelperLicensingScriptId = "kendo-tag-helper-licensing-script-rendered";

        public static T GetService<T>(this ViewContext viewContext)
        {
            return viewContext.HttpContext.GetService<T>();
        }

        public static T GetService<T>(this HttpContext context)
        {
            return (T)context.RequestServices.GetService(typeof(T));
        }

        public static ViewContext ViewContextForType<T>(this ViewContext viewContext, IModelMetadataProvider metadataProvider)
        {
            return new ViewContext(new ActionContext(viewContext.HttpContext, viewContext.RouteData, viewContext.ActionDescriptor), viewData: new ViewDataDictionary<T>(viewContext.ViewData, null), tempData: viewContext.GetService<ITempDataDictionaryFactory>().GetTempData(viewContext.HttpContext), htmlHelperOptions: new HtmlHelperOptions
            {
                ClientValidationEnabled = viewContext.ClientValidationEnabled,
                Html5DateRenderingMode = viewContext.Html5DateRenderingMode,
                ValidationSummaryMessageElement = viewContext.ValidationSummaryMessageElement,
                ValidationMessageElement = viewContext.ValidationMessageElement
            }, view: viewContext.View, writer: new StringWriter());
        }

        public static HtmlHelper<T> CreateHtmlHelper<T>(this ViewContext viewContext)
        {
            IHtmlGenerator service = viewContext.GetService<IHtmlGenerator>();
            ICompositeViewEngine service2 = viewContext.GetService<ICompositeViewEngine>();
            IModelMetadataProvider service3 = viewContext.GetService<IModelMetadataProvider>();
            HtmlEncoder service4 = viewContext.GetService<HtmlEncoder>();
            UrlEncoder service5 = viewContext.GetService<UrlEncoder>();
            Type existingType = TypeInfo.GetExistingType(new TypeInfo[2]
            {
            new TypeInfo
            {
                AssemblyName = "Microsoft.AspNetCore.Mvc.ViewFeatures",
                NS = "Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers",
                TypeName = "IViewBufferScope"
            },
            new TypeInfo
            {
                AssemblyName = "Microsoft.AspNetCore.Mvc.ViewFeatures",
                NS = "Microsoft.AspNetCore.Mvc.ViewFeatures.Internal",
                TypeName = "IViewBufferScope"
            }
            });
            object obj = TypeInfo.InvokeExtensionMethod(typeof(ViewContextExtensions), viewContext, "GetService", new Type[1] { existingType });
            object expressionData = GetExpressionData(viewContext);
            return (HtmlHelper<T>)Activator.CreateInstance(typeof(HtmlHelper<T>), service, service2, service3, obj, service4, service5, expressionData);
        }

        public static string GetFullHtmlFieldName(this ViewContext viewContext, string name)
        {
            return viewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
        }

        internal static void AppendScriptToContext(this ViewContext viewContext, string script, string name)
        {
            viewContext.AppendItemToContext(DeferredScriptsKey, script, name);
        }

        internal static void AppendStylesToContext(this ViewContext viewContext, string style, string name)
        {
            viewContext.AppendItemToContext(DeferredStylesKey, style, name);
        }

        private static void AppendItemToContext(this ViewContext viewContext, string key, string item, string name)
        {
            IDictionary<object, object> items = viewContext.HttpContext.Items;
            KeyValuePair<string, string> item2 = new KeyValuePair<string, string>(name, item);
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            if (items.ContainsKey(key))
            {
                list = (List<KeyValuePair<string, string>>)items[key];
            }
            else
            {
                items[key] = list;
            }

            if (!list.Contains(item2))
            {
                list.Add(item2);
            }
        }

        internal static void AppendItemToContext(this ViewContext viewContext, string key, object value)
        {
            IDictionary<object, object> dictionary = viewContext.HttpContext?.Items;
            if (dictionary != null)
            {
                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
        }

        internal static void RemoveItemFromContext(this ViewContext viewContext, string key, object value)
        {
            viewContext.HttpContext?.Items?.Remove(key);
        }

        internal static void TrackHtmlHelperLicensingScript(this ViewContext viewContext)
        {
            viewContext.AppendItemToContext("kendo-html-helper-licensing-script-rendered", true);
        }

        internal static void UntrackHtmlHelperLicensingScript(this ViewContext viewContext)
        {
            viewContext.AppendItemToContext("kendo-html-helper-licensing-script-rendered", false);
        }

        internal static bool IsHtmlHelperLicensingScriptRendered(this ViewContext viewContext)
        {
            viewContext.HttpContext.Items.TryGetValue("kendo-html-helper-licensing-script-rendered", out object value);
            return (bool)(value ?? ((object)false));
        }

        internal static void TrackTagHelperLicensingScript(this ViewContext viewContext)
        {
            viewContext.AppendItemToContext("kendo-tag-helper-licensing-script-rendered", true);
        }

        internal static void UntrackTagHelperLicensingScript(this ViewContext viewContext)
        {
            viewContext.AppendItemToContext("kendo-tag-helper-licensing-script-rendered", false);
        }

        internal static bool IsTagHelperLicensingScriptRendered(this ViewContext viewContext)
        {
            viewContext.HttpContext.Items.TryGetValue("kendo-tag-helper-licensing-script-rendered", out object value);
            return (bool)(value ?? ((object)false));
        }

        private static object GetExpressionData(ViewContext viewContext)
        {
            Type type = TypeInfo.GetType(new TypeInfo
            {
                AssemblyName = "Microsoft.AspNetCore.Mvc.ViewFeatures",
                NS = "Microsoft.AspNetCore.Mvc.ViewFeatures.Internal",
                TypeName = "ExpressionTextCache"
            });
            if (type != null)
            {
                return Activator.CreateInstance(type);
            }

            Type type2 = TypeInfo.GetType(new TypeInfo
            {
                AssemblyName = "Microsoft.AspNetCore.Mvc.ViewFeatures",
                NS = "Microsoft.AspNetCore.Mvc.ViewFeatures",
                TypeName = "ModelExpressionProvider"
            });
            return TypeInfo.InvokeExtensionMethod(typeof(ViewContextExtensions), viewContext, "GetService", new Type[1] { type2 });
        }
    }
}
