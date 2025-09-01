namespace DynamicMenu.Web.Helper
{
    public interface IKendoOptions
    {
        //
        // Summary:
        //     Indicates whether scripts should be deferred to a script file globally.
        bool DeferToScriptFiles { get; set; }

        //
        // Summary:
        //     Indicates whether scripts should be rendered as JavaScript modules.
        bool RenderAsModule { get; set; }

        //
        // Summary:
        //     Indicates the type of icons to be used by Kendo.
        IconType IconType { get; set; }

        //
        // Summary:
        //     Registers the Kendo services required for DateOnly and TimeOnly support.
        bool UseDateOnlyTimeOnly { get; set; }
    }
}
