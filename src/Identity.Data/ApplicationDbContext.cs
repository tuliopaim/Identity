using System.Reflection;
using Identity.Business.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Perfil, Guid,
        UsuarioPermissao, UsuarioPerfil, IdentityUserLogin<Guid>, PerfilPermissao, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfis { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AtualizarDataDeCriacaoAtualizacao();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void AtualizarDataDeCriacaoAtualizacao()
        {
            var entriesTracked = ChangeTracker
                .Entries()
                .Where(EntidadeAdicionadaOuEditada);

            foreach (var entityEntry in entriesTracked)
            {
                entityEntry.Property("DataDeAtualizacao").CurrentValue = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("DataDeCriacao").CurrentValue = DateTime.Now;
                }
            }
        }

        private static bool EntidadeAdicionadaOuEditada(EntityEntry entry) =>
            entry.Entity is IEntidadeBase &&
                (entry.State == EntityState.Added || entry.State == EntityState.Modified);
    }
}
