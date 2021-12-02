using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class Usuario : IdentityUser<Guid>, IEntidadeBase
    {
        protected Usuario()
        {
        }

        public Usuario(string nome, string email)
        {
            Nome = nome;
            UserName = Email = email;
            Removido = false;
        }

        public string Nome { get; private set; }
        public bool Removido { get; private set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; set; } = new List<UsuarioPerfil>();
        public virtual ICollection<UsuarioPermissao> UsuarioPermissoes { get; set; } = new List<UsuarioPermissao>();
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

                if (usuarioPerfil is null) continue;

                UsuarioPerfis.Remove(usuarioPerfil);
            }
        }

        public void Remover() => Removido = true;
    }
}