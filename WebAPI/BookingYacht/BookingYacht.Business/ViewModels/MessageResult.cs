namespace BookingYacht.Business.ViewModels
{
    public class MessageResult
    {
        public MessageResult(object data, string message)
        {
            Data = data;
            Message = message;
        }

        public object Data { get; set; }
        public string Message { get; set; }
    }
}