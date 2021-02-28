using AplicacaoApp.Interfaces.GenericsApp;
using Entidades;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AplicacaoApp.Interfaces
{
    public interface IProdutoApp : IGenericsApp<Produto>
    {

        Task AdicionarProduto(Produto produto);
        Task AtualizarProduto(Produto produto);
    }
}

