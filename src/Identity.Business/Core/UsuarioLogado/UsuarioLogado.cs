using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Identity.Business.Core.UsuarioLogado
{
    public class UsuarioLogado : IUsuarioLogado
    {
        private readonly HttpContext _httpContext;

        public UsuarioLogado(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string ObterToken()
        {
            return _httpContext.ObterBearerToken();
        }

        public string ObterId()
        {
            return _httpContext.ObterUsuarioLogadoId();
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _httpContext.ObterClaimsToken();
        }
    }
}
