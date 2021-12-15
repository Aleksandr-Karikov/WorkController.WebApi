using System.ComponentModel.DataAnnotations;
using WorkController.WebApi.Requests.Base;

namespace WorkController.WebApi.Requests
{
    public class Login : BaseRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ChiefId { get; set; }

    }
}
