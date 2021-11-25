using Identity.Business.Entities;

namespace Identity.Business.Interfaces.Repositories
{
    public interface IPerfilRepository
    {
        Task<Perfil?> ObterPerfilComPermissoes(Guid id);
    }
}