using Identity.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Data.Configurations
{
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable(nameof(Perfil));

            builder.HasMany(u => u.PerfilUsuarios)
                .WithOne(up => up.Perfil)
                .HasForeignKey(up => up.RoleId)
                .IsRequired();

            builder.HasMany(u => u.PerfilPermissoes)
                .WithOne(up => up.Perfil)
                .HasForeignKey(up => up.RoleId)
                .IsRequired();
        }
    }
}