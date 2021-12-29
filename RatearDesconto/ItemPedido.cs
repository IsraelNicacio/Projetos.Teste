using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatearDesconto
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Desconto { get; set; }
        public decimal Acrescimo { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
