using RatearDesconto.Exceptions;

namespace RatearDesconto.Entidades
{
    public class VendaValidacao
    {
        //Validar total com desconto/acréscimo aplicado
        public static void SubTotalTotalDescontoAcrescimoAplicado(decimal SubTotal, decimal Acrescimo, decimal Desconto, decimal Total, string mensagem)
        {
            decimal totalAcrescimoDescontoAplicado = SubTotal + Acrescimo - Desconto;
            if(totalAcrescimoDescontoAplicado != Total)
                throw new ExcecaoDominio(mensagem);
        }

        public static void DescontoMaiorQueSubTotal(decimal SubTotal, decimal Desconto, string mensagem)
        {
            if(Desconto > SubTotal)
                throw new ExcecaoDominio(mensagem);
        }

        public static void DescontoMaiorQueParaCadaItem(decimal SubTotal, decimal Desconto, int quantidadeItensVenda, decimal VlorMinimoDesconto, string mensagem)
        {
            if (Desconto >= (SubTotal - (quantidadeItensVenda * VlorMinimoDesconto)))
                throw new ExcecaoDominio(mensagem);
        }
    }
}
