//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiWorkControllerServer.IServices;
using WebApiWorkControllerServer.Models;
using WebApiWorkControllerServer.NoDataModels;
using WorkController.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWorkControllerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;
        private IUserService userService;

        public UserController(IOptions<AuthOptions> options, IUserService userService)
        {
            authOptions = options;
            this.userService = userService;
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            var user = await userService.Register(request);
            if (user == null)
            {
                return BadRequest("Возможно пользователь уже существует");
            }
            else return Ok(new { rezult = "Регистрация прошла успешно" });


        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var user = AuthenticatUser(request.Email, request.Password);
            if (user!=null)
            {
                var token = GenerateJwt(user);
                return Ok(new { access_token = token });
            }
            return Unauthorized();
        }
        private User AuthenticatUser(string email,string password)
        {
            return userService.Login(new Login() { Email=email, Password=password});
        }
        private string GenerateJwt(User user)
        {
            var authParams = authOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.ID.ToString())
            };
            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: System.DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials:credentials
                ) ;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
