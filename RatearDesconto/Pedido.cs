using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatearDesconto
{
    public class Pedido
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalDesconto { get; set; }
        public decimal TotalAcrescimo { get; set; }
        public decimal SubTotal { get; set; }
        public List<ItemPedido> itensPedido { get; set; }
    }
}
