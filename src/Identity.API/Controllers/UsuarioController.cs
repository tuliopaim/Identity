using Identity.Business.Entities;
using Identity.Business.Interfaces;
using Identity.Business.Interfaces.Services;
using Identity.Business.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : MainController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioService _userService;
        private readonly IJwtService _jwtService;

        public UsuarioController(
              SignInManager<Usuario> signInManager,
              UserManager<Usuario> userManager,
              IUsuarioService userService,
              INotificador notificador, IJwtService jwtService) : base(notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody]CriarUsuarioRequest registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.RegistrarUsuario(registerUser);

            return CustomResponse(registerUser);
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] LoginUsuarioRequest loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var response = await _userService.AutenticarUsuario(loginUser);

            return CustomResponse(response);
        }

        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaRequest alterarSenha)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _userService.AlterarSenha(alterarSenha);

            return CustomResponse();
        }
    }
}