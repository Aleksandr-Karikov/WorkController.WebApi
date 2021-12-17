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
using WorkController.WebApi.Common;
using WorkController.WebApi.DataBase.Models;
using WorkController.WebApi.IServices;
using WorkController.WebApi.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkController.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        // private readonly IOptions<AuthOptions> authOptions;
        private IUserService userService;

        public UserController(IUserService userService)
        {
            //  authOptions = options;
            this.userService = userService;
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register request)
        {
            var user = await userService.Register(request);
            if (!user.Error.IsNullOrEmpty())
            {
                return BadRequest(user.Error);
            }
            return Ok("Регистрация прошла успешно");


        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login request)
        {
            var ident = userService.Login(request);
            if (ident.Error!=null)
            {
                return BadRequest(ident.Error);
            }
            var identity = GetIdentity(ident);
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
                LastName = ident.LastName,
                FirstName = ident.FirstName,
                Email = ident.Email,
                ID = ident.ID,
                Token = encodedJwt,
            };

            return Ok(response);

        }
        [Route("Alive")]
        [HttpPost]
        public IActionResult Alive()
        {
            return Ok();
        }
        private static ClaimsIdentity GetIdentity(Login user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
       // [Authorize]
        [Route("GetEmployees")]
        [HttpPost]
        public IActionResult GetEmployes(int id)
        {
            return Ok(userService.GetEmployees(id));
        }
        // [Authorize]
        [Route("GetTimes")]
        [HttpPost]
        public IActionResult GetTimes(int id)
        {
            return Ok(userService.GetTimes(id));
        }
        // [Authorize]
        [Route("SetEmployee")]
        [HttpPost]
        public IActionResult SetEmployes(AddEmployee empl)
        {
            var rez = userService.SetNewEmployee(empl);
            if (rez.Error!=null) return BadRequest(rez.Error);
            return Ok("Пользователь добавлен");
        }
        // [Authorize]
        [Route("SetTime")]
        [HttpPost]
        public IActionResult SetTime(TimeRequest time)
        {
            userService.SetTime(time);
            return Ok();
        }
    }

}
