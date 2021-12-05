//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiWorkControllerServer.IServices;
using WebApiWorkControllerServer.Models;
using WebApiWorkControllerServer.NoDataModels;
using WorkController.WebApi.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiWorkControllerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
       // private readonly IOptions<AuthOptions> authOptions;
        private IUserService userService;

        public UserController( IUserService userService)
        {
          //  authOptions = options;
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
            return Ok(new { rezult = "Регистрация прошла успешно" });


        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var identity = GetIdentity(request);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Не правильный логин или пароль" });
            }
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);

        }
       
        private ClaimsIdentity GetIdentity(Login user)
        {
            var ident = userService.Login(user);
            if (ident == null)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        [Authorize]
        [Route("getlogin")]
        [HttpGet]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин");
        }
    }

}
