using Identity.Business.Entities;
using Identity.Business.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Valorem.Core.Infrastructure;

namespace Identity.Data.Repositories
{
    public class PerfilRepository : GenericRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Perfil?> ObterPerfilComPermissoes(Guid id)
        {
            return await GetAsNoTracking()
                .Include(x => x.PerfilPermissoes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> ValidarSeAlgumPerfilEhAdmin(List<Guid>? perfisId)
        {
            var perfilAdminId = await GetAsNoTracking()
                .Where(x => x.Name == "admin")
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return perfisId!.Contains(perfilAdminId);
        }
    }
}
