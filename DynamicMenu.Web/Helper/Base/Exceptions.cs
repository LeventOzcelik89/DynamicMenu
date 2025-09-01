using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DynamicMenu.Web.Helper
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Exceptions
    {
        private static ResourceManager resourceMan;

        private static CultureInfo resourceCulture;

        //
        // Summary:
        //     Returns the cached ResourceManager instance used by this class.
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (resourceMan == null)
                {
                    resourceMan = new ResourceManager("Kendo.Mvc.Resources.Exceptions", typeof(Exceptions).GetTypeInfo().Assembly);
                }

                return resourceMan;
            }
        }

        //
        // Summary:
        //     Overrides the current thread's CurrentUICulture property for all resource lookups
        //     using this strongly typed resource class.
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        //
        // Summary:
        //     Looks up a localized string similar to "{0}" array cannot be empty..
        internal static string ArrayCannotBeEmpty => ResourceManager.GetString("ArrayCannotBeEmpty", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You must use InCell edit mode for batch
        //     updates..
        internal static string BatchUpdatesRequireInCellMode => ResourceManager.GetString("BatchUpdatesRequireInCellMode", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The Update data binding setting is required
        //     for batch updates. Please specify the Update action or url in the DataBinding
        //     configuration..
        internal static string BatchUpdatesRequireUpdate => ResourceManager.GetString("BatchUpdatesRequireUpdate", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to "{0}" cannot be negative..
        internal static string CannotBeNegative => ResourceManager.GetString("CannotBeNegative", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to "{0}" cannot be negative or zero..
        internal static string CannotBeNegativeOrZero => ResourceManager.GetString("CannotBeNegativeOrZero", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to "{0}" cannot be null..
        internal static string CannotBeNull => ResourceManager.GetString("CannotBeNull", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to "{0}" cannot be null or empty..
        internal static string CannotBeNullOrEmpty => ResourceManager.GetString("CannotBeNullOrEmpty", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot find a public property of primitive
        //     type to sort by..
        internal static string CannotFindPropertyToSortBy => ResourceManager.GetString("CannotFindPropertyToSortBy", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot have more one column in order when
        //     sort mode is set to single column..
        internal static string CannotHaveMoreOneColumnInOrderWhenSortModeIsSetToSingleColumn => ResourceManager.GetString("CannotHaveMoreOneColumnInOrderWhenSortModeIsSetToSingleColumn", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot route to class named 'Controller'..
        internal static string CannotRouteToClassNamedController => ResourceManager.GetString("CannotRouteToClassNamedController", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot set AutoBind if widget is populated
        //     during initialization.
        internal static string CannotSetAutoBindIfBoundDuringInitialization => ResourceManager.GetString("CannotSetAutoBindIfBoundDuringInitialization", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot use Ajax and WebService binding
        //     at the same time..
        internal static string CannotUseAjaxAndWebServiceAtTheSameTime => ResourceManager.GetString("CannotUseAjaxAndWebServiceAtTheSameTime", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot use both Detail template and locked
        //     columns.
        internal static string CannotUseDetailTemplateAndLockedColumns => ResourceManager.GetString("CannotUseDetailTemplateAndLockedColumns", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Locked columns are not supported in server
        //     binding mode.
        internal static string CannotUseLockedColumnsAndServerBinding => ResourceManager.GetString("CannotUseLockedColumnsAndServerBinding", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot use PushState with server navigation..
        internal static string CannotUsePushStateWithServerNavigation => ResourceManager.GetString("CannotUsePushStateWithServerNavigation", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot use both Row template and locked
        //     columns.
        internal static string CannotUseRowTemplateAndLockedColumns => ResourceManager.GetString("CannotUseRowTemplateAndLockedColumns", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot use only server templates in Ajax
        //     or WebService binding mode. Please specify a client template as well..
        internal static string CannotUseTemplatesInAjaxOrWebService => ResourceManager.GetString("CannotUseTemplatesInAjaxOrWebService", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Cannot use Virtual Scroll with Server
        //     binding..
        internal static string CannotUseVirtualScrollWithServerBinding => ResourceManager.GetString("CannotUseVirtualScrollWithServerBinding", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to "{0}" collection cannot be empty..
        internal static string CollectionCannotBeEmpty => ResourceManager.GetString("CollectionCannotBeEmpty", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Multiple types were found that match the
        //     controller named '{0}'. This can happen if the route that services this request
        //     does not specify namespaces to search for a controller that matches the request.
        //     If this is the case, register this route by calling an overload of the 'MapRoute'
        //     method that takes a 'namespaces' parameter. The request for '{0}' has found the
        //     following matching controllers:{1}.
        internal static string ControllerNameAmbiguousWithoutRouteUrl => ResourceManager.GetString("ControllerNameAmbiguousWithoutRouteUrl", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Multiple types were found that match the
        //     controller named '{0}'. This can happen if the route that services this request
        //     ('{1}') does not specify namespaces to search for a controller that matches the
        //     request. If this is the case, register this route by calling an overload of the
        //     'MapRoute' method that takes a 'namespaces' parameter. The request for '{0}'
        //     has found the following matching controllers:{2}.
        internal static string ControllerNameAmbiguousWithRouteUrl => ResourceManager.GetString("ControllerNameAmbiguousWithRouteUrl", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Controller name must end with 'Controller'..
        internal static string ControllerNameMustEndWithController => ResourceManager.GetString("ControllerNameMustEndWithController", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Custom command routes is available only
        //     for server binding..
        internal static string CustomCommandRoutesWithAjaxBinding => ResourceManager.GetString("CustomCommandRoutesWithAjaxBinding", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to There is no DataSource Model Id property
        //     specified..
        internal static string DataKeysEmpty => ResourceManager.GetString("DataKeysEmpty", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to DataTable InLine editing and custom EditorTemplate
        //     per column is not supported.
        internal static string DataTableInLineEditingWithCustomEditorTemplates => ResourceManager.GetString("DataTableInLineEditingWithCustomEditorTemplates", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You must specify TemplateName when PopUp
        //     edit mode is enabled with DataTable binding.
        internal static string DataTablePopUpTemplate => ResourceManager.GetString("DataTablePopUpTemplate", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The Delete data binding setting is required
        //     by the delete command. Please specify the Delete action or url in the DataBinding
        //     configuration..
        internal static string DeleteCommandRequiresDelete => ResourceManager.GetString("DeleteCommandRequiresDelete", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The Update data binding setting is required
        //     by the edit command. Please specify the Update action or url in the DataBinding
        //     configuration..
        internal static string EditCommandRequiresUpdate => ResourceManager.GetString("EditCommandRequiresUpdate", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Excel export is not supported in server
        //     binding mode..
        internal static string ExcelExportNotSupportedInServerBinding => ResourceManager.GetString("ExcelExportNotSupportedInServerBinding", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to {0} should not be bigger then {1}..
        internal static string FirstPropertyShouldNotBeBiggerThenSecondProperty => ResourceManager.GetString("FirstPropertyShouldNotBeBiggerThenSecondProperty", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Group with specified name already exists..
        internal static string GroupWithSpecifiedNameAlreadyExists => ResourceManager.GetString("GroupWithSpecifiedNameAlreadyExists", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Group with specified name "{0}" already
        //     exists. Please specify a different name..
        internal static string GroupWithSpecifiedNameAlreadyExistsPleaseSpecifyADifferentName => ResourceManager.GetString("GroupWithSpecifiedNameAlreadyExistsPleaseSpecifyADifferentName", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Group with "{0}" does not exist in {1}
        //     SharedWebAssets..
        internal static string GroupWithSpecifiedNameDoesNotExistInAssetTypeOfSharedWebAssets => ResourceManager.GetString("GroupWithSpecifiedNameDoesNotExistInAssetTypeOfSharedWebAssets", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Group with specified name "{0}" does not
        //     exist. Please make sure you have specified a correct name..
        internal static string GroupWithSpecifiedNameDoesNotExistPleaseMakeSureYouHaveSpecifiedACorrectName => ResourceManager.GetString("GroupWithSpecifiedNameDoesNotExistPleaseMakeSureYouHaveSpecifiedACorrectName", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to InCell editing mode is not supported in
        //     server binding mode.
        internal static string InCellModeNotSupportedInServerBinding => ResourceManager.GetString("InCellModeNotSupportedInServerBinding", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to InCell editing mode is not supported when
        //     ClientRowTemplate is used.
        internal static string InCellModeNotSupportedWithRowTemplate => ResourceManager.GetString("InCellModeNotSupportedWithRowTemplate", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Provided index is out of range..
        internal static string IndexOutOfRange => ResourceManager.GetString("IndexOutOfRange", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The Insert data binding setting is required
        //     by the insert command. Please specify the Insert action or url in the DataBinding
        //     configuration..
        internal static string InsertCommandRequiresInsert => ResourceManager.GetString("InsertCommandRequiresInsert", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Item with specified source already exists..
        internal static string ItemWithSpecifiedSourceAlreadyExists => ResourceManager.GetString("ItemWithSpecifiedSourceAlreadyExists", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Local group with name "{0}" already exists..
        internal static string LocalGroupWithSpecifiedNameAlreadyExists => ResourceManager.GetString("LocalGroupWithSpecifiedNameAlreadyExists", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The key with the following name "{0}"
        //     was not found. Please update all localization files..
        internal static string LocalizationKeyNotFound => ResourceManager.GetString("LocalizationKeyNotFound", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Bound columns require a field or property
        //     access expression..
        internal static string MemberExpressionRequired => ResourceManager.GetString("MemberExpressionRequired", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to {0} should be less than {1}..
        internal static string MinPropertyMustBeLessThenMaxProperty => ResourceManager.GetString("MinPropertyMustBeLessThenMaxProperty", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Name cannot be blank..
        internal static string NameCannotBeBlank => ResourceManager.GetString("NameCannotBeBlank", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Name cannot contain spaces..
        internal static string NameCannotContainSpaces => ResourceManager.GetString("NameCannotContainSpaces", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to "None" is only used for internal purpose..
        internal static string NoneIsOnlyUsedForInternalPurpose => ResourceManager.GetString("NoneIsOnlyUsedForInternalPurpose", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Only one ScriptRegistrar is allowed in
        //     a single request..
        internal static string OnlyOneScriptRegistrarIsAllowedInASingleRequest => ResourceManager.GetString("OnlyOneScriptRegistrarIsAllowedInASingleRequest", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Only one StyleSheetRegistrar is allowed
        //     in a single request..
        internal static string OnlyOneStyleSheetRegistrarIsAllowedInASingleRequest => ResourceManager.GetString("OnlyOneStyleSheetRegistrarIsAllowedInASingleRequest", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Only property and field expressions are
        //     supported.
        internal static string OnlyPropertyAndFieldExpressionsAreSupported => ResourceManager.GetString("OnlyPropertyAndFieldExpressionsAreSupported", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to of {0}.
        internal static string Pager_Of => ResourceManager.GetString("Pager_Of", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Paging must be enabled to use PageOnScroll..
        internal static string PagingMustBeEnabledToUsePageOnScroll => ResourceManager.GetString("PagingMustBeEnabledToUsePageOnScroll", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The {0} must be bigger then 0..
        internal static string PropertyMustBeBiggerThenZero => ResourceManager.GetString("PropertyMustBeBiggerThenZero", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to {0} must be positive number..
        internal static string PropertyMustBePositiveNumber => ResourceManager.GetString("PropertyMustBePositiveNumber", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to {0} should be bigger than {1} and less
        //     then {2}.
        internal static string PropertyShouldBeInRange => ResourceManager.GetString("PropertyShouldBeInRange", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The "{0}" class is no longer supported.
        //     To enable RTL support you must include telerik.rtl.css and apply the "t-rtl"
        //     class to a parent HTML element or the <body>..
        internal static string Rtl => ResourceManager.GetString("Rtl", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Scrolling must be enabled to use PageOnScroll..
        internal static string ScrollingMustBeEnabledToUsePageOnScroll => ResourceManager.GetString("ScrollingMustBeEnabledToUsePageOnScroll", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You must have SiteMap defined with key
        //     "{0}" in ViewData dictionary..
        internal static string SiteMapShouldBeDefinedInViewData => ResourceManager.GetString("SiteMapShouldBeDefinedInViewData", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Source must be a virtual path which should
        //     starts with "~/".
        internal static string SourceMustBeAVirtualPathWhichShouldStartsWithTileAndSlash => ResourceManager.GetString("SourceMustBeAVirtualPathWhichShouldStartsWithTileAndSlash", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Specified file does not exist: "{0}"..
        internal static string SpecifiedFileDoesNotExist => ResourceManager.GetString("SpecifiedFileDoesNotExist", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Passed string cannot be parsed to DateTime
        //     object..
        internal static string StringNotCorrectDate => ResourceManager.GetString("StringNotCorrectDate", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Passed string cannot be parsed to TimeSpan
        //     object..
        internal static string StringNotCorrectTimeSpan => ResourceManager.GetString("StringNotCorrectTimeSpan", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The specified method is not an action
        //     method..
        internal static string TheSpecifiedMethodIsNotAnActionMethod => ResourceManager.GetString("TheSpecifiedMethodIsNotAnActionMethod", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to Time should be bigger than MinTime and
        //     less than MaxTime..
        internal static string TimeOutOfRange => ResourceManager.GetString("TimeOutOfRange", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You should set Tooltip container. Tooltip.For(container).
        internal static string TooltipContainerShouldBeSet => ResourceManager.GetString("TooltipContainerShouldBeSet", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You cannot set Url and ContentUrl at the
        //     same time..
        internal static string UrlAndContentUrlCannotBeSet => ResourceManager.GetString("UrlAndContentUrlCannotBeSet", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The value '{0}' is invalid..
        internal static string ValueNotValidForProperty => ResourceManager.GetString("ValueNotValidForProperty", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to The Url of the WebService must be set.
        internal static string WebServiceUrlRequired => ResourceManager.GetString("WebServiceUrlRequired", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You cannot add more than once column when
        //     sort mode is set to single column..
        internal static string YouCannotAddMoreThanOnceColumnWhenSortModeIsSetToSingle => ResourceManager.GetString("YouCannotAddMoreThanOnceColumnWhenSortModeIsSetToSingle", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You cannot use non generic BindTo overload
        //     without EnableCustomBinding set to true.
        internal static string YouCannotCallBindToWithoutCustomBinding => ResourceManager.GetString("YouCannotCallBindToWithoutCustomBinding", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You cannot call render more than once..
        internal static string YouCannotCallRenderMoreThanOnce => ResourceManager.GetString("YouCannotCallRenderMoreThanOnce", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You cannot call Start more than once..
        internal static string YouCannotCallStartMoreThanOnce => ResourceManager.GetString("YouCannotCallStartMoreThanOnce", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You cannot configure a shared web asset
        //     group..
        internal static string YouCannotConfigureASharedWebAssetGroup => ResourceManager.GetString("YouCannotConfigureASharedWebAssetGroup", resourceCulture);

        //
        // Summary:
        //     Looks up a localized string similar to You must have to call Start prior calling
        //     this method..
        internal static string YouMustHaveToCallStartPriorCallingThisMethod => ResourceManager.GetString("YouMustHaveToCallStartPriorCallingThisMethod", resourceCulture);

        internal static string YouCannotOverrideModelExpressionName => ResourceManager.GetString("YouCannotOverrideModelExpressionName", resourceCulture);

        internal Exceptions()
        {
        }
    }
}
