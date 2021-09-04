using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class Perfil : IdentityRole<Guid>
    {
        public virtual ICollection<UsuarioPerfil> PerfilUsuarios { get; set; }
        public virtual ICollection<PerfilPermissao> PerfilPermissoes { get; set; }

        public bool Administrador => Name == "admin";
    }
}