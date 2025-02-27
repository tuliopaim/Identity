﻿using Identity.Business.Requests;
using Identity.Business.Requests.Usuario;
using Identity.Business.Response;

namespace Identity.Business.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task RegistrarUsuario(CriarUsuarioRequest registerUserRequest);
        Task<LoginResponse?> AutenticarUsuario(LoginUsuarioRequest loginUserRequest);
        Task AlterarSenha(AlterarSenhaRequest alterarSenhaRequest);
        Task AssociarPerfisAoUsuario(AssociarPerfisUsuarioRequest associarPerfisRequest);
        Task DesassociarPerfisAoUsuario(DesassociarPerfisUsuarioRequest associarRequest);
        Task RemoverUsuario(Guid id);
    }
}