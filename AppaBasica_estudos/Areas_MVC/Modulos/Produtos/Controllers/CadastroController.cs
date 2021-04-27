using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Areas_MVC.Areas.Produtos.Controllers
{
    [Area("Produtos")]
    //[Route("produtos/")]
    public class CadastroController : Controller
    {
       // [Route("listagem")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
