using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoApp.Interfaces.GenericsApp
{
    public interface IGenericsApp<T> where T : class
    {
        Task Adicionar(T objeto);
        Task Atualizar(T objeto);
        Task Remover(T objeto);
        Task<T> ObterPorId(Guid id);
        Task<IList<T>> Listar();
        IReadOnlyCollection<Notification> Notifications { get; }
        bool Valid { get; }
        bool Invalid { get; }
    }
}
