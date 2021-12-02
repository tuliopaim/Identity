using Identity.Business.Entities;
using Identity.Business.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Valorem.Core.Infrastructure;

namespace Identity.Data.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterUsuarioPorId(Guid id)
        {
            return await Get().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UsuarioAtivo(string email)
        {
            var usuarioIdRemovido = await GetAsNoTracking()
                .Where(u => u.Email == email)
                .Select(x => new { x.Removido })
                .FirstOrDefaultAsync();

            if (usuarioIdRemovido is null) return false;

            return !usuarioIdRemovido.Removido;
        }

        public async Task<Usuario?> ObterUsuarioComPerfis(Guid id)
        {
            return await Get()
                .Include(u => u.UsuarioPerfis)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UsuarioAdministrador(Guid id)
        {
            return await GetAsNoTracking()
                .AnyAsync(x => x.Id == id && x.UsuarioPerfis.Any(up => up.Perfil.Name == "admin"));
        }
    }
}