using Identity.Business.Entities;
using System;
using System.Threading.Tasks;

namespace Identity.Business.Interfaces.Repositories
{
    public interface IPerfilRepository
    {
        Task<Perfil> ObterPerfilComPermissoes(Guid id);
    }
}