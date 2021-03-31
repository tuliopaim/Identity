using System.Collections.Generic;
using Identity.API.Business.Notificacoes;

namespace Identity.API.Business.Interfaces
{
    public interface INotificador
    {
        void AdicionarNotificacao(Notificacao notificacao);
        List<Notificacao> ObterNotificacoes();
        bool TemNotificacao();
    }
}