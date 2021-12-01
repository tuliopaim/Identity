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

        public async Task<Usuario?> ObterUsuarioComPerfis(Guid usuarioId)
        {
            return await Get()
                .Include(u => u.UsuarioPerfis)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);
        }
    }
}