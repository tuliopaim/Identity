using Identity.Business.Entities;
using Identity.Business.Requests.Perfil;

namespace Identity.Business.Interfaces.Services
{
    public interface IPerfilService
    {
        Task CriarPerfil(CriarPerfilRequest request);
        Task<Perfil?> ObterPerfilComPermissoes(Guid id);
    }
}