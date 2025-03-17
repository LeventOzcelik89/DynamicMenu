namespace DynamicMenu.Core.Models
{
    public class ResultStatus<T>
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public T? objects { get; set; }
        public FeedBack? feedback { get; set; }
    }
}
