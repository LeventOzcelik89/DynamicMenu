using Microsoft.AspNetCore.Mvc;

namespace DynamicMenu.Web.Helper
{
    public interface IUrlGenerator
    {
        string Generate(ActionContext context, INavigatable navigationItem);

        string Generate(ActionContext context, INavigatable navigationItem, RouteValueDictionary routeValues);

        string Generate(ActionContext context, string url);
    }
}
