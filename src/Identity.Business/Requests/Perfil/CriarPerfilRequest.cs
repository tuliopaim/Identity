namespace Identity.Business.Requests.Perfil;

public class CriarPerfilRequest
{
    public string Nome { get; set; }
    public List<CriarPerfilPermissaoRequest> Permissoes { get; set; }
}
