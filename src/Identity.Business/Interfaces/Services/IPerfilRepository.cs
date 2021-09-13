using Identity.Business.Entities;
using System;
using System.Threading.Tasks;

namespace Identity.Business.Interfaces.Services
{
    public interface IPerfilService
    {
        Task<Perfil> ObterPerfilComPermissoes(Guid id);
    }
}