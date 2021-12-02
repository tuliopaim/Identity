using Identity.Business.Entities;
using Identity.Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Perfil> _roleManager;

        public SeedData(
            ApplicationDbContext context,
            UserManager<Usuario> userManager, 
            RoleManager<Perfil> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (await _context.Usuarios.AnyAsync()) return;

            await using var tran = await _context.Database.BeginTransactionAsync();

            try
            {
                await SeedPerfis();
                await SeedUsuarioAdmin();

                await _context.SaveChangesAsync();
                await tran.CommitAsync();
            }
            catch (Exception)
            {
                await tran.RollbackAsync();

                throw;
            }
        }
        
        private async Task SeedPerfis()
        {
            await _roleManager.CreateAsync(new Perfil
            {
                Name = "admin"
            });

            await _roleManager.CreateAsync(new Perfil
            {
                Name = "usuario"
            });
        }

        private async Task SeedUsuarioAdmin()
        {
            var usuarioAdmin = new Usuario("admin", "admin@admin.com")
            {
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(usuarioAdmin, "adm1nPassword");
            await _userManager.AddToRoleAsync(usuarioAdmin, "admin");
        }
    }
}