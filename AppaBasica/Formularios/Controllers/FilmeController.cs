using Formularios.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formularios.Controllers
{
    public class FilmeController : Controller
    {
        public IActionResult Index()
        {
            Filme[] filmes;
            Filme f;
            Adicinar(out filmes, out f);
            Teste(filmes, f);

            for (int i = 0; i < filmes.Length - 1; i++)
            {
                if (filmes[i].Titulo == "Glauber")
                {

                }
            }



            return Ok();
        }

        private static void Adicinar(out Filme[] filmes, out Filme f)
        {
            filmes = new Filme[100];
            f = new Filme
            {
                Titulo = "Glauber"
            };
        }

        private static void Teste(Filme[] filmes, Filme f)
        {
            filmes[70] = f;

            for (int i = 0; i < 99; i++)
            {
                if (i == 70)
                {
                    continue;
                }
                filmes[i] = new Filme
                {
                    Titulo = $"{i} + Teste"
                };
            }
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            Filme filme = new Filme
            {
                Id = 1,
                Titulo = "Teste Filme",
                Descricao = "Descricao Filme"
            };

            return View();
        }
        [HttpPost]
        public IActionResult Adicionar(Filme filme)
        {
            if (ModelState.IsValid)
            {

            }

            return View(filme);
        }
    }
}
