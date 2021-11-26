using Identity.Business.Entities;
using Identity.Business.Interfaces.Services;
using Identity.Business.Requests;
using Identity.Business.Response;
using Microsoft.AspNetCore.Identity;

namespace Identity.Business.Services;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;

    public UserService(
        IJwtService jwtService,
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<ResponseBase> RegistrarUsuario(RegisterUserRequest registerUserRequest)
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
            return ResponseBase.Error(result.Errors.Select(x => x.Description).ToArray());
        }

        await _userManager.AddToRoleAsync(user, "usuario");

        return new ResponseBase();
    }

    public async Task<LoginResponse> AutenticarUsuario(LoginUserRequest loginUserRequest)
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

        var response = new LoginResponse();

        response.AddErrors("Email ou Senha incorretos");

        return response;
    }

    public async Task<ResponseBase> AlterarSenha(AlterarSenhaRequest alterarSenhaRequest)
    {
        var user = await _userManager.FindByNameAsync(alterarSenhaRequest.Email);

        var result = await _userManager.ChangePasswordAsync(
            user,
            alterarSenhaRequest.CurrentPassword,
            alterarSenhaRequest.NewPassword);

        if(result.Succeeded) return new ResponseBase();

        return ResponseBase.Error(result.Errors.Select(x => x.Description).ToArray());
    }
}