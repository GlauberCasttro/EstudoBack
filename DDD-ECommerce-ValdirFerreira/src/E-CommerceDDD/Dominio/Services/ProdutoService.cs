using Dominio.Interfaces.Produtos;
using Dominio.Interfaces.Services;
using Entidades;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dominio.Services
{
    public class ProdutoService : Notifiable, IProdutoService
    {
        private readonly IProdutoRepository _produto;
        private ProdutoValidation _produtoValidation;

        public ProdutoService(IProdutoRepository produto)
        {
            _produto = produto;
        }
        public async Task AdicionarProduto(Produto produto)
        {
            ValidarProduto(produto);


            if (_produtoValidation.Invalid)
            {
                AddNotifications(_produtoValidation.Notifications);
                return;
            }



            produto.Situacao = true;
            await _produto.Adicionar(produto);
        }

        private void ValidarProduto(Produto produto)
        {
            _produtoValidation = new ProdutoValidation(produto);
            _produtoValidation.Validate();
        }

        public async Task AtualizarProduto(Produto produto)
        {
            //var validaNome = produto.ValidaPropriedadeString(produto.Nome, "Nome");
            //var validaValor = produto.ValidarPropriedadeDecimal(produto.Valor, "Valor");

            //if (validaNome && validaValor)
            //{
            await _produto.Atualizar(produto);
            //  }
        }

        public async Task<IList<Produto>> Listar()
        {
            return await _produto.Listar();
        }
    }
}
