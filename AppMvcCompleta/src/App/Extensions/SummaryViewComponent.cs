using DevIo.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Extensions
{
    //View Component para tratar todos os erros quem vem do Businees, utilizando injecao de dependencia com o INotificador que mantem as mensagem que vieram
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

            return View();
        }
    }
}