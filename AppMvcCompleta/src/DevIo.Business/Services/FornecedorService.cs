using DevIo.Business.Interfaces;
using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using DevIo.Business.Notifications;
using DevIo.Business.Validations;
using DevIo.Business.Validations.Documentos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIo.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecoRepository, INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco))
            {
                return;
            }

            var fornecedorBase = _fornecedorRepository.Buscar(e => e.Documento == fornecedor.Documento).Result;
            if (fornecedorBase.Any())
            {
                Notificar($"Já existe um fornecedor de nome {fornecedorBase.FirstOrDefault()?.Nome} cadastrado com esse documento.");
                return;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor))
                return;

            var fornecedorBase = _fornecedorRepository.Buscar(e => e.Documento == fornecedor.Documento).Result.FirstOrDefault();
            if (fornecedorBase != null)
            {
                if (fornecedorBase.Id != fornecedor.Id)
                {
                    Notificar($"Já existe um fornecedor de nome {fornecedorBase?.Nome} cadastrado com esse documento.");
                    return;
                }
            }
            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task AtualizarEndereco(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco))
                return;

            await _enderecoRepository.Atualizar(fornecedor.Endereco);
        }

        public async Task Remover(Guid id)
        {
            var result = _fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result;

            if (result.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados, com isso nao é possível excluir o mesmo");
                return;
            }

            await _enderecoRepository.Remover(result.Endereco.Id);

            await _fornecedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
        }
    }
}