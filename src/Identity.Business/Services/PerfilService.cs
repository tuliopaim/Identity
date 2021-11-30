using Identity.Business.Entities;
using Identity.Business.Interfaces.Services;
using Identity.Business.Interfaces.Repositories;
using Identity.Business.Requests.Perfil;
using Microsoft.AspNetCore.Identity;
using Identity.Business.Interfaces;
using System.Security.Claims;

namespace Identity.Business.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly INotificador _notificador;
        private readonly IPerfilRepository _repository;
        private readonly RoleManager<Perfil> _roleManager;

        public PerfilService(
            INotificador notificador,
            IPerfilRepository repository,
            RoleManager<Perfil> roleManager)
        {
            _notificador = notificador;
            _repository = repository;
            _roleManager = roleManager;
        }

        public async Task CriarPerfil(CriarPerfilRequest request)
        {
            var perfil = new Perfil
            {
                Name = request.Nome
            };

            var result = await _roleManager.CreateAsync(perfil);

            if (!result.Succeeded)
            {
                _notificador.AdicionarNotificacoes(result.Errors.Select(e => e.Description));
                return;
            }

            await AdicionarPermissoes(perfil, request.Permissoes);
        }

        private async Task AdicionarPermissoes(Perfil perfil, List<CriarPerfilPermissaoRequest> permissoes)
        {
            var resultList = new List<IdentityResult>();

            foreach (var permissao in permissoes)
            {
                var permissoesStr = string.Join(string.Empty, permissao.Permissoes.Select(p => p.ToString()));
                var claim = new Claim(permissao.Nome.ToString(), permissoesStr);

                var identityResult = await _roleManager.AddClaimAsync(perfil, claim);
                resultList.Add(identityResult);
            }

            foreach (var erro in resultList.Where(r => !r.Succeeded).SelectMany(r => r.Errors))
            {
                _notificador.AdicionarNotificacao(erro.Description);
            }
        }

        public async Task<Perfil?> ObterPerfilComPermissoes(Guid id)
        {
            return await _repository.ObterPerfilComPermissoes(id);
        }
    }
}
