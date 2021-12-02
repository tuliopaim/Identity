using Identity.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));
            
            builder.HasQueryFilter(u => !u.Removido);

            builder
                .Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.HasMany(u => u.UsuarioPermissoes)
                .WithOne(up => up.Usuario)
                .HasForeignKey(up => up.UserId)
                .IsRequired();

            builder.HasMany(u => u.UsuarioPerfis)
                .WithOne(up => up.Usuario)
                .HasForeignKey(up => up.UserId)
                .IsRequired();
        }
    }
}