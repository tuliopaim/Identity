using Identity.Business.ViewModels;

namespace Identity.Business.Interfaces.Services
{
    public interface IJwtService
    {
        Task<LoginResponseViewModel> GerarReponseComToken(string email);
    }
}