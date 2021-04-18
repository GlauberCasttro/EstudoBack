using App.ViewModels;
using AutoMapper;
using DevIo.Business.Interfaces;
using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using DevIo.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("fornecedores/")]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
            IMapper mapper,
            IEnderecoRepository enderecoRepository,
            IFornecedorService fornecedorService, INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
            _fornecedorService = fornecedorService;
            _notificador = notificador;
        }

        [Route("lista-fornecedores")]
        public async Task<IActionResult> Index()
        {

            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }

        [Route("{id}/detalhes-fornecedor")]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(fornecedorViewModel);
            }

            fornecedorViewModel.RemoveCaracteresDocumento();
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);

            if (!OperacaoValida())
                return View(fornecedorViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Route("{id}/atualizar-fornecedor")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("{id}/atualizar-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(fornecedorViewModel);

            fornecedorViewModel.RemoveCaracteresDocumento();
            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            return RedirectToAction(nameof(Index));
        }

        [Route("{id}/excluir-fornecedor")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null) return NotFound();

            return PartialView("_ConfirmaDelete", fornecedorViewModel);
        }

        [Route("{id}/excluir-fornecedor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            await _fornecedorService.Remover(id);

            if (!OperacaoValida())
            {
                if (_notificador.TemNotificacao())
                {
                    foreach (var item in _notificador.ObterNotificacoes())
                    {
                        TempData["Erro"] += $"\n{item.Mensagem}";
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("{id}/obter-endereco-fornecedor")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [Route("{id}/atualizar-endereco-fornecedor")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            return PartialView("_AtualizaEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [Route("{id}/atualizar-endereco-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedor)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");
            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", fornecedor);

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Fornecedor>(fornecedor));

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedor.Endereco.FornecedorId });

            return Json(new { success = true, url });
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}