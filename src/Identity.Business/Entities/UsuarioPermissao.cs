using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class UsuarioPermissao : IdentityUserClaim<Guid>
    {
        public virtual Usuario Usuario { get; set; }
        public DateTime DataDeCriacao { get; private set; }
    }
}