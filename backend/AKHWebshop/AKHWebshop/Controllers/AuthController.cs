#nullable enable
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AKHWebshop.Models;
using AKHWebshop.Models.Auth;
using AKHWebshop.Models.Http.Request;
using AKHWebshop.Models.Http.Request.Concrete;
using AKHWebshop.Models.Http.Response;
using AKHWebshop.Services.Auth;
using AKHWebshop.Services.DTO;
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
        private IActionResultFactory<JsonResult> _jsonResponseFactory;
        private IRequestMapper _requestMapper;

        public AuthController(
            UserManager<AppUser> userManager,
            IActionResultFactory<JsonResult> jsonResponeFactory,
            IRequestMapper requestMapper,
            JwtTokenHelper tokenHelper
        )
        {
            _userManager = userManager;
            _jsonResponseFactory = jsonResponeFactory;
            _requestMapper = requestMapper;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUserRequest request)
        {
            AppUser user = _requestMapper.RegisterRequestToAppUser(request);
            var result = await _userManager.CreateAsync(user, user.Password);
            return _jsonResponseFactory.CreateResponse(200, result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            AppUser user = await _userManager.FindByNameAsync(request.UserName);

            string token =
                _tokenHelper
                    .GenerateToken(new[]
                    {
                        new Claim(ClaimTypes.Name, user!.UserName),
                    });

            HttpContext.Response.Cookies.Append(K.userAuthCookieName, token);
            return _jsonResponseFactory.CreateResponse(200, "ok");
        }
    }
}