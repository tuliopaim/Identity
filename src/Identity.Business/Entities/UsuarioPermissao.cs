using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class UsuarioPermissao : IdentityUserClaim<Guid>, IEntidadeBase
    {
        public virtual Usuario Usuario { get; set; }
        public DateTime DataDeCriacao { get; private set; }
        public DateTime DataDeAtualizacao { get; private set; }
    }
}