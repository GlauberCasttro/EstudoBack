using DevIo.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIo.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}
