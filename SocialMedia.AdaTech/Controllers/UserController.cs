using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.AdaTech.Filters;

namespace AppAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public UserController(ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(NotLoggedInFilter))]
        public IActionResult Login(Credential credencial)
        {
            try
            {
                var user = _userService.ReadUserByEmail(credencial.Email);

                if (user is null || user.Password != credencial.Password)
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
            } catch(Exception ex)
            {
                return BadRequest("Não foi possível fazer o login: " + ex.Message);
            }
        }

        [HttpPost("logout")]
        [ServiceFilter(typeof(VerifyJWTFilter))]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok("Usuário deslogado com sucesso!");
        }

        [HttpPost("addUser")]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] UserRequest request)
        {
            try
            {
                var user = _userService.AddUser(request);
                return Ok(new { Message = "Usuário criado com sucesso!", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("readUserByName")]
        [ServiceFilter(typeof(VerifyJWTFilter))]
        public IActionResult ReadUserByName([FromQuery] string name)
        {
            try
            {
                var user = _userService.ReadUserByName(name);
                return Ok(new { Message = "Usuário encontrado com sucesso!", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("readUserByEmail")]
        [ServiceFilter(typeof(VerifyJWTFilter))]
        public IActionResult ReadUserByEmail([FromQuery] string email)
        {
            try
            {
                var user = _userService.ReadUserByEmail(email);
                return Ok(new { Message = "Usuário encontrado com sucesso!", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("readUserByID")]
        [ServiceFilter(typeof(VerifyJWTFilter))]
        public IActionResult ReadUserByID([FromQuery] int id)
        {
            try
            {
                var user = _userService.ReadUserByID(id);
                return Ok(new { Message = "Usuário encontrado com sucesso!", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("readAllUsers")]
        [ServiceFilter(typeof(VerifyJWTFilter))]
        public IActionResult ReadAllUsers()
        {
            try
            {
                var users = _userService.ReadAllUsers();
                return Ok(new { Message = "Usuários encontrados com sucesso!", User = users });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateUser")]
        [ServiceFilter(typeof(VerifyJWTFilter))]
        public IActionResult UpdateUser([FromBody] UserRequest request, [FromQuery] int id)
        {
            try
            {
                var user = _userService.UpdateUser(request, id);
                return Ok(new { Message = "Usuário atualizado com sucesso!", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteUser")]
        [ServiceFilter(typeof(VerifyJWTFilter))]
        public IActionResult DeleteUser([FromQuery] int id)
        {
            try
            {
                var user = _userService.DeleteUser(id);
                return Ok(new { Message = "Usuário deletado com sucesso!", User = user });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
