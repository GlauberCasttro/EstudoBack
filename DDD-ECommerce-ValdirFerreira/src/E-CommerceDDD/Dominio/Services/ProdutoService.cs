using Dominio.Interfaces.Produtos;
using Dominio.Interfaces.Services;
using Entidades;
using Flunt.Notifications;
using System.Threading.Tasks;

namespace Dominio.Services
{
    public class ProdutoService : Notifiable, IProdutoService
    {
        private readonly IProduto _Iproduto;
        public ProdutoService(IProduto produto)
        {
            _Iproduto = produto;
        }
        public async Task AdicionarProduto(Produto produto)
        {
            //if (produto.Invalid)
            //{
            //    AddNotifications(produto.Notifications);
            //    return;
            //}

            produto.Situacao = true;
            await _Iproduto.Adicionar(produto);
        }

        public async Task AtualizarProduto(Produto produto)
        {
            //var validaNome = produto.ValidaPropriedadeString(produto.Nome, "Nome");
            //var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");

            //if (validaNome && validaValor)
            //{
                await _Iproduto.Atualizar(produto);
          //  }
        }
    }
}
