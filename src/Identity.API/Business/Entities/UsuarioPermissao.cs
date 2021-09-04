using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Business.Entities
{
    public class UsuarioPermissao : IdentityUserClaim<Guid>
    {
        public virtual Usuario Usuario { get; set; }
    }
}