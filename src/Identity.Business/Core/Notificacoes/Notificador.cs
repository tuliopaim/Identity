using Identity.Business.Interfaces;

namespace Identity.Business.Core.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void AdicionarNotificacao(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public void AdicionarNotificacao(string notificacao)
        {
            AdicionarNotificacao(new Notificacao(notificacao));
        }

        public void AdicionarNotificacoes(IEnumerable<string> notificacoes)
        {
            foreach (var notificacao in notificacoes)
            {
                AdicionarNotificacao(notificacao);
            }
        }

        public void AdicionarNotificacoes(IEnumerable<Notificacao> notificacoes)
        {
            foreach (var notificacao in notificacoes)
            {
                AdicionarNotificacao(notificacao);
            }
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
