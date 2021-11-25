using Identity.Business.Entities;

namespace Identity.Business.Interfaces.Services
{
    public interface IPerfilService
    {
        Task<Perfil> ObterPerfilComPermissoes(Guid id);
    }
}