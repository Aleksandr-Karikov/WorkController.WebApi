using WorkController.WebApi.Requests.Base;

namespace WorkController.WebApi.Requests
{
    public class SetPeriodRequest:BaseRequest
    {
        public int Time { get; set; }
        public int UserId { get; set; }
    }
}
