using WorkController.WebApi.Requests.Base;

namespace WorkController.WebApi.Requests
{
    public class AddEmployee: BaseRequest
    {
        public string Email { get; set; }

        public int ChiefId { get; set; }
    }
}
