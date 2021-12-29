using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatearDesconto.Entidades
{
    public class Item
    {
        public int Id { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal Acrescimo { get; private set; }
        public decimal Total { get; private set; }
        public decimal FatorMultiplicacao { get; private set; }

        public Item(int id, string codigo, string descricao, decimal quantidade, decimal valorUnitario, decimal subTotal, decimal desconto, decimal acrescimo, decimal total, decimal fatorMultiplicacao)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            SubTotal = subTotal;
            Desconto = desconto;
            Acrescimo = acrescimo;
            Total = total;
            FatorMultiplicacao = fatorMultiplicacao;
        }

        public void DescontoAtualizar(decimal valor) => Desconto = valor;
        public void AcrescimoAtualizar(decimal valor) => Acrescimo = valor;
        public void TotalAtualizar(decimal valor) => Total = valor;
    }
}
