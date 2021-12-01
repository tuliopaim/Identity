using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Identity.Business.Core.UsuarioLogado
{
    public static class HttpContextExtensions
    {
        public static string ObterUsuarioLogadoId(this HttpContext httpContext)
        {
            var claim = httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static T ObterService<T>(this HttpContext httpContext)
        {
            var service = (T)httpContext.RequestServices.GetService(typeof(T));
            return service;
        }

        public static string ObterBearerToken(this HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return null;
            };

            var token = httpContext.Request.Headers[HeaderNames.Authorization].ToString();
            token = token.Replace("Bearer", string.Empty).Trim();
            return token;
        }

        public static IEnumerable<Claim> ObterClaimsToken(this HttpContext httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return null;
            };

            return httpContext.User.Claims;
        }
    }
}
