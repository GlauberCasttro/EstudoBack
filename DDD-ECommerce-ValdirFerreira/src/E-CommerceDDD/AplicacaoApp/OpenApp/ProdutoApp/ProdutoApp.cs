using AplicacaoApp.Interfaces;
using Dominio.Interfaces.Produtos;
using Dominio.Interfaces.Services;
using Entidades;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AplicacaoApp.OpenApp
{
    public class ProdutoApp : Notifiable, IProdutoApp
    {
        private readonly IProdutoRepository _produto;
        private readonly IProdutoService _produtoService;

        public ProdutoApp(IProdutoRepository produto, IProdutoService produtoService)
        {
            _produto = produto;
            _produtoService = produtoService;
        }
        public async Task Adicionar(Produto objeto)
        {
            await _produto.Adicionar(objeto);
        }

        public async Task AdicionarProduto(Produto produto)
        {
            await _produtoService.AdicionarProduto(produto);

            if (_produtoService.Invalid)
            {
                AddNotifications(_produtoService.Notifications);
            }
        }
        public async Task AtualizarProduto(Produto produto)
        {
            await _produtoService.AtualizarProduto(produto);
        }

        public async Task Atualizar(Produto objeto)
        {
            await _produto.Atualizar(objeto);
        }

        public async Task<IList<Produto>> Listar()
        {
            return await _produtoService.Listar();
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _produto.ObterPorId(id);
        }

        public async Task Remover(Produto objeto)
        {
            await _produto.Remover(objeto);
        }
    }
}
