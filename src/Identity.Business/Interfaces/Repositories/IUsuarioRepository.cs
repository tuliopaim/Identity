using Identity.Business.Entities;

namespace Identity.Business.Interfaces.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> ObterUsuarioComPerfis(Guid usuarioId);
    }
}