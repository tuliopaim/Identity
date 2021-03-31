using System.Threading.Tasks;
using Identity.API.ViewModels;

namespace Identity.API.Business.Interfaces
{
    public interface IJwtService
    {
        Task<LoginResponseViewModel> GerarReponseComToken(string email);
    }
}