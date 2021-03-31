using System.Linq;
using System.Threading.Tasks;
using Identity.API.Entities;
using Identity.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody]RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser()
            {
                Name = registerUser.Name,
                UserName = registerUser.Email,
                Email = registerUser.Email
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = 
                await _signInManager.PasswordSignInAsync(
                    loginUser.Email,
                    loginUser.Password,
                    false,
                    false);

            if (!result.Succeeded)
            {
                return BadRequest("Email ou Senha incorreto(a).");
            }

            return Ok();
        }

    }
}