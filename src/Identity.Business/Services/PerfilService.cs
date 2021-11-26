using Identity.Business.Entities;
using Identity.Business.Interfaces.Services;
using Identity.Business.Interfaces.Repositories;

namespace Identity.Business.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _repository;

        public PerfilService(IPerfilRepository repository)
        {
            _repository = repository;
        }

        public async Task<Perfil> ObterPerfilComPermissoes(Guid id)
        {
            return await _repository.ObterPerfilComPermissoes(id);
        }
    }
}
