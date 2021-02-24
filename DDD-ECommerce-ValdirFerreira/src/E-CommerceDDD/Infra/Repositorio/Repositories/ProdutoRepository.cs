using Dominio.Interfaces.Produtos;
using Entidades;

namespace Infra.Repositorio
{
    public class ProdutoRepository : RepositoryGenerics<Produto>, IProduto
    {
    }
}
