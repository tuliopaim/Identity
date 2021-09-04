using System.Threading.Tasks;
using Identity.Business.Entities;
using Identity.Business.Interfaces;
using Identity.Business.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IJwtService _jwtService;

        public AuthController(
              SignInManager<Usuario> signInManager,
              UserManager<Usuario> userManager,
              INotificador notificador, IJwtService jwtService) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody]RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new Usuario()
            {
                Name = registerUser.Name,
                UserName = registerUser.Email,
                Email = registerUser.Email,
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                return CustomResponse("Usuario criado com sucesso");
            }

            foreach (var erro in result.Errors)
            {
                NotificarErro(erro.Description);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(
                loginUser.Email,
                loginUser.Password,
                false,
                false);

            if (result.Succeeded)
            {
                var responseViewModel = await _jwtService.GerarReponseComToken(loginUser.Email);
                return CustomResponse(responseViewModel);
            }

            NotificarErro("Email ou Senha incorretos");
            return CustomResponse(loginUser);
        }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = await _userManager.FindByNameAsync(viewModel.Email);

            var result = await _userManager
                .ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

            if (result.Succeeded)
            {
                return CustomResponse("Senha alterada com sucesso!");
            }

            NotificarErro("Senha atual incorreta");
            return CustomResponse();
        }
    }
}