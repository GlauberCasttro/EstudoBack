using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIo.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AplicacaoContext contexto) : base(contexto)
        {

        }
        public Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return _context.Fornecedores
                .Include(e => e.Endereco)
                .AsNoTracking()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await _context.Fornecedores
                .Include(e => e.Produtos)
                 .Include(ed => ed.Endereco)
                .Where(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
