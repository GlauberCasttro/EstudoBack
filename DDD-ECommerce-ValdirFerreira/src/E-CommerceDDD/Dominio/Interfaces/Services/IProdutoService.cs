using Entidades;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Services
{
    public interface IProdutoService
    {
        bool Valid { get; }
        bool Invalid { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
        Task AdicionarProduto(Produto produto);
        Task AtualizarProduto(Produto produto);
    }
}
