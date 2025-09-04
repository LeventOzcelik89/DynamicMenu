using DynamicMenu.Core.Models;

namespace DynamicMenu.API
{
    public static class Extensions
    {

        public static string ToJson(this FeedBack feedback)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(feedback);
        }

    }
}
