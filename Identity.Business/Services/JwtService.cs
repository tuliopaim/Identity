﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Business.Entities;
using Identity.Business.Interfaces.Services;
using Identity.Business.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Business.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Perfil> _roleManager;
        private readonly AppSettings _appSettings;

        public JwtService(
            UserManager<Usuario> userManager,
            RoleManager<Perfil> roleManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }

        public async Task<LoginResponseViewModel> GerarReponseComToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = (await _userManager.GetClaimsAsync(user))?.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            await AdicionarClaims(claims, user, userRoles);
            
            var encodedToken = CriarToken(claims);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims?.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private async Task AdicionarClaims(List<Claim> claims, Usuario user, IEnumerable<string> userRoles)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                if(roleClaims != null) claims.AddRange(roleClaims);
            }
        }

        private string CriarToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}