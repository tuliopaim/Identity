using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class Usuario : IdentityUser<Guid>, IEntidadeBase
    {
        public string Name { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; set; }
        public virtual ICollection<UsuarioPermissao> UsuarioPermissoes { get; set; }
        public DateTime DataDeCriacao { get; private set; }
        public DateTime DataDeAtualizacao { get; private set; }

        public void AssociarPerfis(List<Guid> perfisId)
        {
            UsuarioPerfis ??= new List<UsuarioPerfil>();

            foreach (var perfilId in perfisId)
            {
                if (UsuarioPerfis.Any(p => p.RoleId == perfilId)) continue;

                UsuarioPerfis.Add(new UsuarioPerfil()
                {
                    UserId = Id,
                    RoleId = perfilId
                });
            }
        }

        public void DesassociarPerfis(List<Guid> perfisId)
        {
            UsuarioPerfis ??= new List<UsuarioPerfil>();

            foreach (var perfilId in perfisId)
            {
                var usuarioPerfil = UsuarioPerfis.FirstOrDefault(x => x.RoleId == perfilId);

                if(usuarioPerfil is null) continue;

                UsuarioPerfis.Remove(usuarioPerfil);
            }
        }
    }
}