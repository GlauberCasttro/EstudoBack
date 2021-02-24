using AplicacaoApp.Interfaces.GenericsApp;
using Entidades;
using System.Threading.Tasks;

namespace AplicacaoApp.Interfaces
{
    public interface IProdutoApp : IGenericsApp<Produto>
    {
        Task AdicionarProduto(Produto produto);
        Task AtualizarProduto(Produto produto);
    }
}

