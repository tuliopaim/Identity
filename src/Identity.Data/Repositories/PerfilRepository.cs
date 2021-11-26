using Identity.Business.Entities;
using Identity.Business.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly ApplicationDbContext _context;

        public PerfilRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Perfil?> ObterPerfilComPermissoes(Guid id)
        {
            return await _context.Perfis
                .AsNoTracking()
                .Include(x => x.PerfilPermissoes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
