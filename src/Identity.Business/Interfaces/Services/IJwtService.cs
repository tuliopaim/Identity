using Identity.Business.Response;

namespace Identity.Business.Interfaces.Services
{
    public interface IJwtService
    {
        Task<LoginResponse> GerarReponseComToken(string email);
    }
}