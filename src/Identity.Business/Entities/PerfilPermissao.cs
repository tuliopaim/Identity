using Identity.Business.Enumeradores;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class PerfilPermissao : IdentityRoleClaim<Guid>, IEntidadeBase
    {
        public DateTime DataDeCriacao { get; private set; }
        public DateTime DataDeAtualizacao { get; private set; }
        public virtual Perfil Perfil { get; set; }

        public IEnumerable<PermissaoEnum> Permissoes
        {
            get
            {
                var enumClaimValues = Enum.GetValues(typeof(PermissaoEnum)).Cast<PermissaoEnum>();
                var permissoes = enumClaimValues.Where(p => ClaimValue.Contains(p.ToString()));

                return permissoes;
            }
        }
    }
}