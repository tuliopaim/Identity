using Identity.Business.Entities;

namespace Identity.Business.Interfaces.Repositories
{
    public interface IPerfilRepository : IGenericRepository<Perfil>
    {
        Task<Perfil?> ObterPerfilComPermissoes(Guid id);
        Task<bool> ValidarSeAlgumPerfilEhAdmin(List<Guid>? perfisId);
    }
}