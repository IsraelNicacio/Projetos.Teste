using RatearDesconto.Auxiliar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatearDesconto.Entidades
{
    public class Venda
    {
        public DateTime DataHora { get; private set; }
        public VendaStatus Status { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal Acrescimo { get; private set; }
        public decimal Total { get; private set; }
        public string ReferenciaVersao { get; private set; }
        public string ReferenciaOrigem { get; private set; }
        public string ReferenciaCodigo { get; private set; }
        public List<Item> Itens { get; private set; }

        public Venda(DateTime dataHora, VendaStatus status, decimal subTotal, decimal desconto, decimal acrescimo, decimal total, string referenciaVersao, string referenciaOrigem, string referenciaCodigo)
        {
            DataHora = dataHora;
            Status = status;
            SubTotal = subTotal;
            Desconto = desconto;
            Acrescimo = acrescimo;
            Total = total;
            ReferenciaVersao = referenciaVersao;
            ReferenciaOrigem = referenciaOrigem;
            ReferenciaCodigo = referenciaCodigo;
        }

        public void DescontoAtualizar(decimal valor) => Desconto = valor;
        public void AcrescimoAtualizar(decimal valor) => Acrescimo = valor;
        public void TotalAtualizar(decimal valor) => Total = valor;
        public void AdicionarItem(Item item) => Itens.Add(item);
        public void AdicionarListaItens(List<Item> itens) => Itens = itens;
    }
}
