using DevIo.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIo.Business.Interfaces.Repositories
{
    /// <summary>
    /// Interface de fornecedor
    /// </summary>
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
    }
}
