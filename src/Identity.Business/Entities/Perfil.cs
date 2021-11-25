using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class Perfil : IdentityRole<Guid>, IEntidadeBase
    {
        public virtual ICollection<UsuarioPerfil> PerfilUsuarios { get; set; }
        public virtual ICollection<PerfilPermissao> PerfilPermissoes { get; set; }
        public DateTime DataDeCriacao { get; private set; }
        public DateTime DataDeAtualizacao { get; private set; }
        public bool Administrador => Name == "admin";
    }
}