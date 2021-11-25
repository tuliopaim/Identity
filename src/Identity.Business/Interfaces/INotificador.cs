using Identity.Business.Notificacoes;

namespace Identity.Business.Interfaces
{
    public interface INotificador
    {
        void AdicionarNotificacao(Notificacao notificacao);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
    }
}