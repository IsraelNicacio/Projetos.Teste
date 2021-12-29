using System;
using System.Linq;
using System.Collections.Generic;

namespace RatearDesconto.Entidades
{
    public static class ItensCalculo
    {
        public static decimal RatearDesconto(IList<Item> itens, decimal pedidoTotalDesconto)
        {
            #region Aplicar rateio do desconto

            decimal totalDescontoAplicado = 0;

            foreach (Item item in itens)
            {
                //Calcula desconto do item
                item.DescontoAtualizar(decimal.Round(pedidoTotalDesconto * item.FatorMultiplicacao, 2, MidpointRounding.AwayFromZero));

                if (item.Desconto < 0.01M)
                    item.DescontoAtualizar(0);

                //Recalcula total do item
                item.TotalAtualizar(item.SubTotal - item.Desconto);

                //Soma total aplicado até o momento
                totalDescontoAplicado += item.Desconto;

                //Verifica se o total aplicado é igual ao total de desconto do pedido
                if (totalDescontoAplicado == pedidoTotalDesconto)
                    break;
            }

            #endregion Aplicar rateio do desconto

            #region Aplicar diferença do rateio do desconto

            if (totalDescontoAplicado < pedidoTotalDesconto)
            {
                decimal diferencaDesconto = pedidoTotalDesconto - totalDescontoAplicado;

                //Verifica se tem algum item que ainda NÃO recebeu desconto e que possa aplicar o valor restante de desconto com no mínimo 0,01
                var item = itens.FirstOrDefault(x => x.Desconto == 0 && (x.SubTotal - diferencaDesconto) >= 0.01M);
                if (item == null)
                    //Verifica se tem algum item que JÁ recebeu desconto e que possa aplicar o valor restante de desconto com no mínimo 0,01
                    item = itens.FirstOrDefault(x => x.Desconto != 0 && (x.Total - diferencaDesconto) >= 0.01M);

                //Caso não tenha identificado nenhum item capaz de receber a diferença de desconto
                ItemValidacao.SeNulo(item, "Não encontrado item para aplicação da diferença de desconto");

                //Calcula desconto do item
                item.DescontoAtualizar(item.Desconto + diferencaDesconto);

                //Recalcula total do item
                item.TotalAtualizar(item.SubTotal - item.Desconto);

                //Soma total aplicado até o momento com a diferença
                totalDescontoAplicado += diferencaDesconto;
            }

            #endregion Aplicar diferença do rateio do desconto

            return totalDescontoAplicado;
        }
    }
}
