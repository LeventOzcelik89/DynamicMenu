namespace DynamicMenu.Core.Models
{
    public class FeedBack
    {
        public string action { get; set; } = "";
        public string title { get; set; } = "Sistem Uyarısı";
        public string status { get; set; } = "success";
        public int timeout { get; set; } = 8;
        public string message { get; set; }

        public static FeedBack Create(string message, bool issuccess = false)
        {
            return new FeedBack
            {
                message = message.Replace(Environment.NewLine, "<br />"),
                status = (issuccess ? "success" : "error")
            };
        }

        public static FeedBack Success(string? message)
        {
            return new FeedBack
            {
                message = message?.Replace(Environment.NewLine, "<br />")
            };
        }

        public static FeedBack Error(string? message)
        {
            return new FeedBack
            {
                message = message?.Replace(Environment.NewLine, "<br />"),
                status = "error"
            };
        }

    }
}
