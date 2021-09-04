using System.Threading.Tasks;
using Identity.Business.ViewModels;

namespace Identity.Business.Interfaces
{
    public interface IJwtService
    {
        Task<LoginResponseViewModel> GerarReponseComToken(string email);
    }
}