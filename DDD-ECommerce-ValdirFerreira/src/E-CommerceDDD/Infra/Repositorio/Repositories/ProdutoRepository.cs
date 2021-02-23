using Dominio.Interfaces.Produtos;
using Entidades.Entidades;

namespace Infra.Repositorio
{
    public class ProdutoRepository : RepositoryGenerics<Produto>, IProduto
    {
    }
}
