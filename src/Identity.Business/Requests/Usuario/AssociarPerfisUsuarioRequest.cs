using System.ComponentModel.DataAnnotations;

namespace Identity.Business.Requests.Usuario
{
    public class AssociarPerfisUsuarioRequest
    {        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid UsuarioId { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public List<Guid>? PerfisId { get; set; }
    }
}
