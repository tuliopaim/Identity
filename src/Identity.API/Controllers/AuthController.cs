using Identity.Business.Entities;
using Identity.Business.Interfaces;
using Identity.Business.Interfaces.Services;
using Identity.Business.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(
              SignInManager<Usuario> signInManager,
              UserManager<Usuario> userManager,
              IUserService userService,
              INotificador notificador, IJwtService jwtService) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody]RegisterUserRequest registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var response = await _userService.RegistrarUsuario(registerUser);

            if (response.Valido)
            {
                return CustomResponse("Usuario criado com sucesso");
            }

            foreach (var erro in response.Erros)
            {
                NotificarErro(erro);
            }

            return CustomResponse(registerUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var response = await _userService.AutenticarUsuario(loginUser);

            if (response.Valido) return CustomResponse(response);

            NotificarErros(response.Erros);

            return CustomResponse(loginUser);
        }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaRequest alterarSenha)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var response = await _userService.AlterarSenha(alterarSenha);

            if (response.Valido)
            {
                return CustomResponse("Senha alterada com sucesso!");
            }

            NotificarErros(response.Erros);

            return CustomResponse();
        }
    }
}