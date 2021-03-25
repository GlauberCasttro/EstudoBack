using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Areas_MVC.Modulos.Vendas.Controllers
{
    [Area("Vendas")]
    //[Route("pedidos/")]
    public class PedidoController : Controller
    {
        [Route("pedidos")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
