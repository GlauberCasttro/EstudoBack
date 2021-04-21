using App.Extensions;
using App.ViewModels;
using AutoMapper;
using DevIo.Business.Interfaces;
using DevIo.Business.Interfaces.Repositories;
using DevIo.Business.Models;
using DevIo.Business.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("fornecedores/")]
    [Authorize]
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
        [ClaimsAuthorize("Fornecedor", "Consultar")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }

        [Route("{id}/detalhes-fornecedor")]
        [ClaimsAuthorize("Fornecedor", "Consultar")]
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
        [ClaimsAuthorize("Fornecedor", "Gravar")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-fornecedor")]
        [ClaimsAuthorize("Fornecedor", "Gravar")]
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
        [ClaimsAuthorize("Fornecedor", "Editar")]
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
        [ClaimsAuthorize("Fornecedor", "Editar")]
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
        [ClaimsAuthorize("Fornecedor", "Excluir")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null) return NotFound();

            return PartialView("_ConfirmaDelete", fornecedorViewModel);
        }

        [Route("{id}/excluir-fornecedor")]
        [ClaimsAuthorize("Fornecedor", "Excluir")]
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
                        TempData["Erro"] = item.Mensagem;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [Route("{id}/obter-endereco-fornecedor")]
        [ClaimsAuthorize("Fornecedor", "Consultar")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [Route("{id}/atualizar-endereco-fornecedor")]
        [ClaimsAuthorize("Fornecedor", "Consultar")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            return PartialView("_AtualizaEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("{id}/atualizar-endereco-fornecedor")]
        [HttpPost]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizaEndereco", fornecedorViewModel);

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Fornecedor>(fornecedorViewModel));

            if (!OperacaoValida()) return PartialView("_AtualizaEndereco", fornecedorViewModel);

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
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