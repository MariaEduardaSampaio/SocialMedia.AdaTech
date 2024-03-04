using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.AdaTech.Entities;
using SocialMedia.AdaTech.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;

        public UserController(IConfiguration configuration, TokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(Credencial credencial)
        {
            // 1. Buscar um usuário que tenha o mesmo username e senha que a credencial no banco

            if (!_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("jwt"))
            {
                if (credencial.Email != "duda@gmail.com" && credencial.Senha != "123Seguro&")
                    return NotFound("Usuário ou senha incorretos.");
                else
                {
                    var chaveToken = _tokenService.GerarToken(credencial);

                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddDays(7)
                    };
                    Response.Cookies.Append("jwt", chaveToken, cookieOptions);

                    return Ok("Usuário logado com sucesso!");
                }
            }
            else
                return BadRequest("Usuário já está logado.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("jwt"))
            {
                Response.Cookies.Delete("jwt");
                return Ok("Usuário deslogado com sucesso!");
            }
            else
                return BadRequest("Usuário precisa estar logado.");
        }

        [HttpGet("darboasvindas")]
        public IActionResult DarBoasVindas()
        {
            return Ok("Bem-vindo ao nosso sistema!");
        }
    }
}
