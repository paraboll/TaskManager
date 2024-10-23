using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TM.WebServer.Entities;
using TM.WebServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TM.WebServer.Factories;

namespace TM.WebServer.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class TestController: ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly Application.Interfaces.IAuthorizationService _authorizationService;

        public TestController(ILogger<TestController> logger, 
            IJwtTokenService jwtTokenService,
            Application.Interfaces.IAuthorizationService authorizationService)
        {
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _authorizationService = authorizationService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordDomainAuthentication([FromBody] UIAuthConfig auth)
        {
            _logger.LogTrace($"Вызван метод API login для {auth.Login}");

#if DEBUG
            if (auth.Login != "sa" || auth.Password != "sa") //TODO: После тестов убрать.
            {
                return Unauthorized();
            }
#elif RELEASE
                var isAuth = await _authorizationService.AuthorizationAsync(auth.Login, auth.Password);
                if (!isAuth)
                {
                    return Unauthorized();
                }
#endif

            _logger.LogInformation($"Пользователь ({auth.Login}) прошел авторизацию.");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, auth.Login),
            };

            return Ok(
                ResponseFactory.GoodResponse(new
                {
                    access_token = _jwtTokenService.GetJwtToken(claims),
                    expires_in = _jwtTokenService.GetTokenTime()
                })
            );
        }

        [HttpGet("isServiceWorking")]
        [AllowAnonymous]
        public async Task<IActionResult> IsServiceWorking()
        {
            _logger.LogInformation($"Вызван метод API test");

            return Ok(ResponseFactory.GoodResponse("Ok"));
        }

        [HttpGet("badRequest")]
        [AllowAnonymous]
        public async Task<IActionResult> badRequest()
        {
            return BadRequest();
        }


        [HttpGet("throwException")]
        [AllowAnonymous]
        public async Task<IActionResult> ThrowException()
        {
            throw new Exception("Произошло исключение.");
        }
    }
}
