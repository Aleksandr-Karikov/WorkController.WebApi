using System.ComponentModel.DataAnnotations;

namespace WebApiWorkControllerServer.DataBase.Models.NoDataModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
