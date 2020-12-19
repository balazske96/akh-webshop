using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AKHWebshop.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AKHWebshop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private JwtTokenHelper _tokenHelper;

        public AuthController(UserManager<AppUser> userManager, JwtTokenHelper tokenHelper)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        public async Task<JsonResult> Regiser([FromBody] AppUser user)
        {
            var result = await _userManager.CreateAsync(user, user.Password);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("login")]
        public JsonResult Login([FromBody] AppUser inputForm)
        {
            AppUser? user = _userManager.FindByNameAsync(inputForm.UserName).Result;
            bool userNotFount = user == null;
            if (userNotFount)
                return new JsonResult(new {error = "username or password wrong"});

            string token = _tokenHelper.GenerateToken(
                new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                });

            this.HttpContext.Response.Cookies.Append("_uc", token);

            return new JsonResult("ok")
            {
                ContentType = "application/json", StatusCode = 200
            };
        }
    }
}