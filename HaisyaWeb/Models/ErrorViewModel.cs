namespace HaisyaWeb.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(Message);
        public string Layout { get; set; }
    }
}
