﻿using System.Security.Claims;
using Identity.Business.Enumeradores;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Extensions.Attributes
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimsRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }

        public ClaimsAuthorizeAttribute(PermissaoNomeEnum permissaoNome, PermissaoValorEnum permissaoValor) 
            : this(permissaoNome.ToString(), permissaoValor.ToString())
        {
        }
    }
}