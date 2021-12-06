using System.ComponentModel.DataAnnotations;
using WorkController.WebApi.Requests.Base;

namespace WebApiWorkControllerServer.NoDataModels
{
    public class Login:BaseRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
    }
}
