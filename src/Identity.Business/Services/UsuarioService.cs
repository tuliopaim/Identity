using Identity.Business.Entities;
using Identity.Business.Interfaces;
using Identity.Business.Interfaces.Repositories;
using Identity.Business.Interfaces.Services;
using Identity.Business.Requests;
using Identity.Business.Requests.Usuario;
using Identity.Business.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPerfilRepository _perfilRepository;
    private readonly INotificador _notificador;
    private readonly IJwtService _jwtService;
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public UsuarioService(
        IUsuarioRepository usuarioRepository,
        IPerfilRepository perfilRepository,
        INotificador notificador,
        IJwtService jwtService,
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager)
    {
        _usuarioRepository = usuarioRepository;
        _perfilRepository = perfilRepository;
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

        if (result.Succeeded) return;

        _notificador.AdicionarNotificacoes(result.Errors.Select(x => x.Description));
    }

    public async Task AssociarPerfisAoUsuario(AssociarPerfisUsuarioRequest associarRequest)
    {
        if(!associarRequest!.PerfisId!.Any())return;

        var usuario = await _usuarioRepository.ObterUsuarioComPerfis(associarRequest.UsuarioId);

        if (usuario == null)
        {
            _notificador.AdicionarNotificacao($"Usuario não encontrado!");
            return;
        }
        
        if(await _perfilRepository.ValidarSeAlgumPerfilEhAdmin(associarRequest.PerfisId))
        {
            _notificador.AdicionarNotificacao($"Não é possivel atribuir o perfil de administrador!");
            return;
        }
        
        usuario.AssociarPerfis(associarRequest!.PerfisId!);

        await _usuarioRepository.UnitOfWork.CommitAsync();
    }
}