using AplicacaoApp.Interfaces;
using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebEcommerce.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly IProdutoApp _produtoApp;
        public ProdutoController(IProdutoApp produtoApp)
        {
            _produtoApp = produtoApp;
        }
        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoApp.Listar();
            return View(produtos);
        }

        // GET: ProdutoController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await _produtoApp.ObterPorId(id);
            return View(produto);
        }

        // GET: ProdutoController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                await _produtoApp.AdicionarProduto(produto);
                if (_produtoApp.Invalid)
                {
                    foreach (var errpr in _produtoApp.Notifications)
                    {
                        ModelState.AddModelError(errpr.Property, errpr.Message);
                    }
                    return View("Create", produto);
                }
            }
            catch
            {
                return View("Create", produto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutoController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await _produtoApp.ObterPorId(id);
            return View(produto);
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Produto produto)
        {
            try
            {
                await _produtoApp.AtualizarProduto(produto);
                if (_produtoApp.Invalid)
                {
                    foreach (var errpr in _produtoApp.Notifications)
                    {
                        ModelState.AddModelError(errpr.Property, errpr.Message);
                    }
                    return View("Edit", produto);
                }
            }
            catch
            {
                return View("Edit", produto);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {

            var produto = await _produtoApp.ObterPorId(id);
            return View(produto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, Produto produto)
        {
            try
            {
                var produtoDelete = await _produtoApp.ObterPorId(id);
                await _produtoApp.Remover(produtoDelete);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
