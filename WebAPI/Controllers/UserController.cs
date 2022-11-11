using Application.Applications;
using Application.Interfaces;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Token;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUser _IApplicationUser;
        private readonly UserManager<Entities.Entities.ApplicationUser> _userManager;
        private readonly SignInManager<Entities.Entities.ApplicationUser> _signInManager;

        public UserController(IApplicationUser IApplicationUser, SignInManager<Entities.Entities.ApplicationUser> signInManager,
            UserManager<Entities.Entities.ApplicationUser> userManager)
        {
            _IApplicationUser = IApplicationUser;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Unauthorized();

            var result = await _IApplicationUser.CheckUser(login.email, login.password);
            if (result)
            {
                var userId = await _IApplicationUser.ReturnIdUser(login.email);
                var token = new TokenJWTBuilder()
                     .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                 .AddSubject("Company - SamDev085")
                 .AddIssuer("Test.Securiry.Bearer")
                 .AddAudience("Test.Securiry.Bearer")
                 .AddClaim("userId", userId)
                 .AddExpiry(60) // minutes for a token expiration
                 .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Ok("Falta alguns dados");

            var result = await
                _IApplicationUser.AddUser(login.email, login.password, login.age, login.phone);

            if (result)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao adicionar usuário");
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Unauthorized();

            var result = await
                _signInManager.PasswordSignInAsync(login.email, login.password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userId = await _IApplicationUser.ReturnIdUser(login.email);
                var token = new TokenJWTBuilder()
                     .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                 .AddSubject("Company - SamDev085")
                 .AddIssuer("Test.Securiry.Bearer")
                 .AddAudience("Test.Securiry.Bearer")
                 .AddClaim("userId", userId)
                 .AddExpiry(30) // minutes for a token expiration
                 .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
                return Ok("Falta alguns dados");

            var user = new Entities.Entities.ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                Phone = login.phone,
                Age = login.age,
                Type = UserType.CommonUser
            };
            var result = await _userManager.CreateAsync(user, login.password);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            // Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // retorno email 
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result2 = await _userManager.ConfirmEmailAsync(user, code);

            if (result2.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");
            else
                return Ok("Erro ao confirmar usuários");

        }
    }
}
