using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIo.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AplicacaoContext context) : base(context)
        {

        }
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await _context.Enderecos
                .AsNoTracking()
                .Where(e => e.FornecedorId == fornecedorId)
                .FirstOrDefaultAsync();
        }
    }
}
