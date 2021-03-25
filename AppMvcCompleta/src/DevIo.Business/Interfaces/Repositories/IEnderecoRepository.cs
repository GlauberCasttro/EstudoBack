using DevIo.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIo.Business.Interfaces.Repositories
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}
