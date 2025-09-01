
namespace DynamicMenu.Web.Model
{
    public class RemoteServiceDynamicMenuAPI : RemoteServiceBase
    {
        public override string baseAddress => "https://localhost:7059/api/";
        public RemoteServiceDynamicMenuAPI(HttpClient httpClient) : base(httpClient) { }
    }
}
