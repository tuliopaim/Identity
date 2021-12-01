using System.Reflection;
using Identity.Business.Entities;
using Identity.Business.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Perfil, Guid,
        UsuarioPermissao, UsuarioPerfil, IdentityUserLogin<Guid>, PerfilPermissao, IdentityUserToken<Guid>>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Perfil> Perfis => Set<Perfil>();
        public DbSet<UsuarioPerfil> UsuarioPerfis  => Set<UsuarioPerfil>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Ignore<IdentityUserLogin<Guid>>();

            DefineTiposPadrao(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private static void DefineTiposPadrao(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                                .SelectMany(t => t.GetProperties())
                                .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(string)))
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetColumnType("timestamp without time zone");
                }
                else
                {
                    property.SetColumnType("varchar(200)");
                }
            }
        }
                
        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            AtualizarDataDeCriacaoAtualizacao();

            return (await SaveChangesAsync(cancellationToken)) > 0;
        }

        private void AtualizarDataDeCriacaoAtualizacao()
        {
            var entriesTracked = ChangeTracker
                .Entries()
                .Where(EntidadeAdicionadaOuEditada);

            foreach (var entityEntry in entriesTracked)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property("DataDeAtualizacao").CurrentValue = DateTime.Now;
                }
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
