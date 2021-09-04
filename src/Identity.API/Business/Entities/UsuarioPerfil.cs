using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Business.Entities
{
    public class UsuarioPerfil : IdentityUserRole<Guid>
    {
        public virtual Perfil Perfil { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}