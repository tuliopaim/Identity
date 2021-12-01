using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.Business.Core.UsuarioLogado
{
    public interface IUsuarioLogado
    {
        IEnumerable<Claim> ObterClaims();
        string ObterId();
        string ObterToken();
    }
}