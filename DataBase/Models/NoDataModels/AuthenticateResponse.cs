using WebApiWorkControllerServer.Models;

namespace WebApiWorkControllerServer.DataBase.Models.NoDataModels
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.ID;
            Name = user.Name;
            LastName = user.LastName;
            MiddleName = user.MiddleName;
            Login = user.Login;
            Email = user.Email;
            Token = token;
        }
        //public string Name { get; set; }
        //[Column(TypeName = "nvarchar(50)")]
        //public string LastName { get; set; }
        //[Column(TypeName = "nvarchar(50)")]
        //public string MiddleName { get; set; }
        //[Column(TypeName = "nvarchar(50)")]
        //public string Email { get; set; }
        //[Column(TypeName = "nvarchar(50)")]
        //[Required]
        //public string Login { get; set; }
        //[Column(TypeName = "nvarchar(200)")]
        //[Required]
        //public string Password { get; set; }
        //[Column(TypeName = "int")]
        //public int? ChiefId { get; set; }
        //[ForeignKey("ChiefId")]
        //public User Chief { get; set; }
    }
}
