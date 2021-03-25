using Areas_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Areas_MVC.Data
{
    public interface IPedidoRepository
    {
       Pedido ObterPedido();
    }
    public class PedidoRepository : IPedidoRepository
    {
        public Pedido ObterPedido()
        {
            return new Pedido();
        }
    }
}
