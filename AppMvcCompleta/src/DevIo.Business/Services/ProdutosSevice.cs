using DevIo.Business.Interfaces;
using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using DevIo.Business.Notifications;
using DevIo.Business.Validations.Documentos;
using System;
using System.Threading.Tasks;

namespace DevIo.Business.Services
{
    public class ProdutosSevice : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosSevice(IProdutoRepository produtoRepository, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }


        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }
        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}