using Identity.API.Extensions.Attributes;
using Identity.Business.Enumeradores;
using Identity.Business.Interfaces;
using Identity.Business.Interfaces.Services;
using Identity.Business.Requests.Perfil;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/perfil")]
    public class PerfilController : MainController
    {
        private readonly IPerfilService _perfilService;

        public PerfilController(INotificador notificador, IPerfilService perfilService) : base(notificador)
        {
            _perfilService = perfilService;
        }

        [ClaimsAuthorize(PermissaoNomeEnum.Perfil, PermissaoValorEnum.C)]
        [HttpPost]
        public async Task<IActionResult> CriarPerfil(CriarPerfilRequest request)
        {
            await _perfilService.CriarPerfil(request);
             
            return CustomResponse();
        }
    }
}
