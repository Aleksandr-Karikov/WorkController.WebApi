using WorkController.WebApi.Requests.Base;

namespace WorkController.WebApi.Requests
{
    public class EmployeeMoney: BaseRequest
    {
        public int Money { get; set; }
        public int Id { get; set; }
    }

}
