using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Business.Entities
{
    public class UsuarioLogin : IdentityUserLogin<Guid>
    {
        public virtual Usuario Usuario { get; set; }
    }
}