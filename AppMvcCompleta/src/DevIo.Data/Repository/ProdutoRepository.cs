using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIo.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(AplicacaoContext context) : base(context)
        {

        }
        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await _context.Produtos
                .AsNoTracking()
                .Include(e => e.Fornecedor)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await _context.Produtos
                .AsNoTracking()
                .Include(e => e.Fornecedor)
                .OrderBy((p => p.Nome))
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }

        public async Task<IEnumerable<Produto>> ObterProdutoPorNome(string nome)
        {
            return await _context.Produtos.Include(e => e.Fornecedor).Where(e => e.Nome.Contains(nome)).ToListAsync();
        }
    }
}
