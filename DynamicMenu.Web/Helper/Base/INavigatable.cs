namespace DynamicMenu.Web.Helper
{
    public interface INavigatable
    {
        //
        // Summary:
        //     Gets or sets the name of the route.
        //
        // Value:
        //     The name of the route.
        string RouteName { get; set; }

        //
        // Summary:
        //     Gets or sets the name of the controller.
        //
        // Value:
        //     The name of the controller.
        string ControllerName { get; set; }

        //
        // Summary:
        //     Gets or sets the name of the action.
        //
        // Value:
        //     The name of the action.
        string ActionName { get; set; }

        //
        // Summary:
        //     Gets the route values.
        //
        // Value:
        //     The route values.
        RouteValueDictionary RouteValues { get; }

        //
        // Summary:
        //     Gets or sets the URL.
        //
        // Value:
        //     The URL.
        string Url { get; set; }
    }
}
