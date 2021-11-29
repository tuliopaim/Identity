using Identity.Business.Entities;
using Identity.Business.Interfaces;
using Identity.Business.Interfaces.Services;
using Identity.Business.Requests;
using Identity.Business.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Services;

public class UserService : IUserService
{
    private readonly INotificador _notificador;
    private readonly IJwtService _jwtService;
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public UserService(
        INotificador notificador,
        IJwtService jwtService,
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager)
    {
        _notificador = notificador;
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task RegistrarUsuario(CriarUsuarioRequest registerUserRequest)
    {
        var user = new Usuario()
        {
            Name = registerUserRequest.Name,
            UserName = registerUserRequest.Email,
            Email = registerUserRequest.Email,
        };

        var result = await _userManager.CreateAsync(user, registerUserRequest.Password);

        if (!result.Succeeded)
        {
            _notificador.AdicionarNotificacoes(result.Errors.Select(x => x.Description));
            return;
        }

        await _userManager.AddToRoleAsync(user, "usuario");
    }

    public async Task<LoginResponse?> AutenticarUsuario(LoginUsuarioRequest loginUserRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(
            loginUserRequest.Email,
            loginUserRequest.Password,
            false,
            false);

        if (result.Succeeded)
        {
            var jwtResponse = await _jwtService.GerarReponseComToken(loginUserRequest.Email);

            return jwtResponse;
        }

        _notificador.AdicionarNotificacao("Email ou Senha incorretos");

        return null;
    }

    public async Task AlterarSenha(AlterarSenhaRequest alterarSenhaRequest)
    {
        var user = await _userManager.FindByNameAsync(alterarSenhaRequest.Email);

        var result = await _userManager.ChangePasswordAsync(
            user,
            alterarSenhaRequest.CurrentPassword,
            alterarSenhaRequest.NewPassword);

        if(result.Succeeded) return;

        _notificador.AdicionarNotificacoes(result.Errors.Select(x => x.Description));
    }
}