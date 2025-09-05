namespace DynamicMenu.Core.Models
{
    public class ResultStatus<T>
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string? message { get; set; }
        public T? objects { get; set; }
        public FeedBack? feedback { get; set; }

        public static ResultStatus<T> Error(string? message = null)
        {
            message = message ?? "Beklenmedik bir sorun oluştu.";
            return new ResultStatus<T>
            {
                message = message,
                feedback = FeedBack.Error(message)
            };
        }

        public static ResultStatus<T> Success(T data, string? message = null)
        {
            message = message ?? "İşlem başarıyla tamamlandı.";
            return new ResultStatus<T>
            {
                message = message,
                success = true,
                objects = data,
                feedback = FeedBack.Success(message),
            };
        }

    }
}
