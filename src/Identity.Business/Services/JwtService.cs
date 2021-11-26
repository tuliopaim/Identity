using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Business.Entities;
using Identity.Business.Interfaces.Services;
using Identity.Business.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Business.Services
{
    public class JwtService : IJwtService
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> GerarReponseComToken(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(usuario);

            var claims = claimsPrincipal.Claims.ToList();

            AdicionarClaims(claims);

            var expiracao = int.Parse(_configuration["AppSettings:ExpiracaoHoras"]);

            var encodedToken = CriarToken(claims, expiracao);

            var response = new LoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(expiracao).TotalSeconds,
                UserToken = new UserTokenResponse
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    Claims = claims.Select(c => new ClaimResponse
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                }
            };

            return response;
        }

        private static void AdicionarClaims(List<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
        }

        private string CriarToken(IEnumerable<Claim> claims, int expiracao)
        {
            var secrect = _configuration["AppSettings:Secret"];
            var emissor = _configuration["AppSettings:Emissor"];
            var validoEm = _configuration["AppSettings:ValidoEm"];

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secrect);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = emissor,
                Audience = validoEm,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(expiracao),
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