using Identity.Business.Requests;
using Identity.Business.Response;

namespace Identity.Business.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResponseBase> RegistrarUsuario(RegisterUserRequest registerUserRequest);
        Task<LoginResponse> AutenticarUsuario(LoginUserRequest loginUserRequest);
        Task<ResponseBase> AlterarSenha(AlterarSenhaRequest alterarSenhaRequest);
    }
}