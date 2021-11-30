using Identity.Business.Enumeradores;

namespace Identity.Business.Requests.Perfil;

public class CriarPerfilPermissaoRequest
{
    public PermissaoNomeEnum Nome { get; set; }
    public List<PermissaoValorEnum> Permissoes { get; set; }
}
