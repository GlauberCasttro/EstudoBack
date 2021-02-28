using Dominio.Interfaces.Produtos;
using Entidades;
using Infrastructure.Configuration;

namespace Infra.Repositorio
{
    public class ProdutoRepository : RepositoryGenerics<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ContextBase context) : base(context) { }
    }
}
