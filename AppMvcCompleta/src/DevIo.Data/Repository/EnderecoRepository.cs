using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using DevIo.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
