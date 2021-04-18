using System;
using System.Collections.Generic;
using System.Text;

namespace DevIo.Business.Notifications
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);

    }
}
