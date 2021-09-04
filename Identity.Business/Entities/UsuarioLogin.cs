using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class UsuarioLogin : IdentityUserLogin<Guid>
    {
        public virtual Usuario Usuario { get; set; }
    }
}