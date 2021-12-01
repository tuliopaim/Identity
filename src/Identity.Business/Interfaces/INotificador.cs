using Identity.Business.Core.Notificacoes;

namespace Identity.Business.Interfaces
{
    public interface INotificador
    {
        void AdicionarNotificacoes(IEnumerable<string> notificacoes);
        void AdicionarNotificacoes(IEnumerable<Notificacao> notificacoes);
        void AdicionarNotificacao(Notificacao notificacao);
        void AdicionarNotificacao(string notificacao);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
    }
}