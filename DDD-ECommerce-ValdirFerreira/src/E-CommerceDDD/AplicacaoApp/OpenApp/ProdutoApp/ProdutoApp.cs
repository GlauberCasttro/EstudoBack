using AplicacaoApp.Interfaces;
using Dominio.Interfaces.Produtos;
using Dominio.Interfaces.Services;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AplicacaoApp.OpenApp
{
    public class ProdutoApp : IProdutoApp
    {
        private readonly IProduto _produto;
        private readonly IProdutoService _produtoService;

        public ProdutoApp(IProduto produto, IProdutoService produtoService)
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
        }
        public async Task AtualizarProduto(Produto produto)
        {
            await _produtoService.AtualizarProduto(produto);
        }

        public async Task Atualizar(Produto objeto)
        {
            await _produto.Atualizar(objeto);
        }

        public Task<List<Produto>> Listar()
        {
            return _produto.Listar();
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
