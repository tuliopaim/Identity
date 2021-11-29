using Identity.Business.Requests;
using Identity.Business.Response;

namespace Identity.Business.Interfaces.Services
{
    public interface IUserService
    {
        Task RegistrarUsuario(CriarUsuarioRequest registerUserRequest);
        Task<LoginResponse> AutenticarUsuario(LoginUsuarioRequest loginUserRequest);
        Task AlterarSenha(AlterarSenhaRequest alterarSenhaRequest);
    }
}