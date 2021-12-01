using Identity.Business.Entities;
using Identity.Business.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Valorem.Core.Infrastructure;

namespace Identity.Data.Repositories
{
    public class PerfilRepository : GenericRepository<Perfil>, IPerfilRepository
    {
        private readonly ApplicationDbContext _context;

        public PerfilRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Perfil?> ObterPerfilComPermissoes(Guid id)
        {
            return await GetAsNoTracking()
                .Include(x => x.PerfilPermissoes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
