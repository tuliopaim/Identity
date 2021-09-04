using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Entities
{
    public class PerfilPermissao : IdentityRoleClaim<Guid>
    {
        public PerfilPermissao()
        { }

        public PerfilPermissao(Guid roleId, string claimType, string claimValue)
        {
            RoleId = roleId;
            ClaimType = claimType;
            ClaimValue = claimValue ?? "";
        }

        public PerfilPermissao(Guid roleId, string claimType, List<string> claimValues)
        {
            RoleId = roleId;
            ClaimType = claimType;

            ListToClaimValue(claimValues);
        }
        public DateTime DataDeCriacao { get; private set; }
        public virtual Perfil Perfil { get; set; }

        public void AtualizarPermissoes(List<string> permissoes)
        {
            ListToClaimValue(permissoes);
        }

        private void ListToClaimValue(List<string> permissoes)
        {
            ClaimValue = permissoes is null
                ? ""
                : string.Join(",", permissoes);
        }

        public void AdicionarPermissoes(List<string> permissoes)
        {
            permissoes?.ForEach(AdicionarPermissao);
        }

        public void AdicionarPermissao(string permissao)
        {
            var listaPermissoes = ClaimValueToList();

            if (listaPermissoes.Contains(permissao)) return;

            listaPermissoes.Add(permissao);

            ListToClaimValue(listaPermissoes);
        }

        public void RemoverPermissoes(List<string> permissoes)
        {
            permissoes?.ForEach(RemoverPermissao);
        }

        public void RemoverPermissao(string permissao)
        {
            var listaPermissoes = ClaimValueToList();

            if (!listaPermissoes.Contains(permissao)) return;

            listaPermissoes.Remove(permissao);

            ListToClaimValue(listaPermissoes);
        }

        private List<string> ClaimValueToList()
        {
            return string.IsNullOrEmpty(ClaimValue)
                ? new List<string>()
                : ClaimValue.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}