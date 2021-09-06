using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class UsuarioPerfil : IdentityUserRole<Guid>
    {
        public virtual Perfil Perfil { get; set; }
        public virtual Usuario Usuario { get; set; }
        public DateTime DataDeCriacao { get; private set; }
    }
}