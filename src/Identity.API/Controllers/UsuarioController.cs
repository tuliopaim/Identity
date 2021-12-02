using Identity.API.Extensions.Attributes;
using Identity.Business.Enumeradores;
using Identity.Business.Interfaces;
using Identity.Business.Interfaces.Services;
using Identity.Business.Requests;
using Identity.Business.Requests.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioService _userService;

        public UsuarioController(
              IUsuarioService userService,
              INotificador notificador) : base(notificador)
        {
            _userService = userService;
        }

        [HttpPost("registrar")]
        [ClaimsAuthorize(PermissaoNomeEnum.Usuario, PermissaoValorEnum.C)]
        public async Task<IActionResult> Registrar([FromBody] CriarUsuarioRequest registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.RegistrarUsuario(registerUser);

            return CustomResponse(registerUser);
        }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] LoginUsuarioRequest loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var response = await _userService.AutenticarUsuario(loginUser);

            return CustomResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("alterar-senha")]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaRequest alterarSenha)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _userService.AlterarSenha(alterarSenha);

            return CustomResponse();
        }

        [HttpPost("associar-perfis")]
        [ClaimsAuthorize(PermissaoNomeEnum.UsuarioPerfil, PermissaoValorEnum.C)]
        public async Task<IActionResult> AssociarPerfis([FromBody] AssociarPerfisUsuarioRequest associarPerfisRequest)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.AssociarPerfisAoUsuario(associarPerfisRequest);

            return CustomResponse();
        }

        [HttpPost("desassociar-perfis")]
        [ClaimsAuthorize(PermissaoNomeEnum.UsuarioPerfil, PermissaoValorEnum.C)]
        public async Task<IActionResult> DesassociarPerfis([FromBody] DesassociarPerfisUsuarioRequest desassociarPerfisRequest)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.DesassociarPerfisAoUsuario(desassociarPerfisRequest);

            return CustomResponse();
        }

        [HttpDelete("{id:guid}")]
        [ClaimsAuthorize(PermissaoNomeEnum.Usuario, PermissaoValorEnum.R)]
        public async Task<IActionResult> Remover(Guid id)
        {
            await _userService.RemoverUsuario(id);

            return CustomResponse();
        }
    }
}