using System;
using System.Threading.Tasks;
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

        public SeedData(
            ApplicationDbContext context,
            UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (await _context.Usuarios.AnyAsync()) return;

            await using var tran = await _context.Database.BeginTransactionAsync();

            try
            {
                var adminId = Guid.NewGuid();
                var perfilAdminId = Guid.NewGuid();

                await SeedUsuarioAdmin(adminId);
                SeedPerfis(perfilAdminId);
                SeedUsuarioPerfis(adminId, perfilAdminId);

                await _context.SaveChangesAsync();
                await tran.CommitAsync();
            }
            catch (Exception)
            {
                await tran.RollbackAsync();

                throw;
            }
        }

        private async Task SeedUsuarioAdmin(Guid adminId)
        {
            await _userManager.CreateAsync(new Usuario
            {
                Id = adminId,
                Name = "admin",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            }, "adm1nPassword");
        }

        private void SeedPerfis(Guid perfilAdminId)
        {
            _context.Perfis.Add(new Perfil
            {
                Id = perfilAdminId,
                Name = "admin"
            });
        }

        private void SeedUsuarioPerfis(Guid adminId, Guid perfilAdminId)
        {
            _context.UsuarioPerfis.Add(new UsuarioPerfil
            {
                UserId = adminId,
                RoleId = perfilAdminId
            });
        }
    }
}