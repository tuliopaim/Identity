using Identity.Business.Entities;

namespace Identity.Business.Interfaces.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> ObterUsuarioPorId(Guid usuarioId);
        Task<Usuario?> ObterUsuarioComPerfis(Guid usuarioId);
        Task<bool> UsuarioAtivo(string email);
        Task<bool> UsuarioAdministrador(Guid id);
    }
}