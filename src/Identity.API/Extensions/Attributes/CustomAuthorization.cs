﻿namespace Identity.API.Extensions.Attributes
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {
            return (context.User.Identity?.IsAuthenticated ?? false) &&
                context.User.IsInRole("admin") || 
                context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }
}