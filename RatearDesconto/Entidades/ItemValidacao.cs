using RatearDesconto.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatearDesconto.Entidades
{
    public class ItemValidacao
    {
        public static void SeNulo(object obj, string mensagem)
        {
            if(obj == null)
                throw new ExcecaoDominio(mensagem);
        }

        public static void SeListaNula(List<object> listObj, string mensagem)
        {
            if (listObj == null)
                throw new ExcecaoDominio(mensagem);
        }

        public static void SeListaVazia(List<object> listObj, string mensagem)
        {
            if (listObj.Count == 0)
                throw new ExcecaoDominio(mensagem);
        }

        public static void SeDiferente(decimal totalDescontoAplicado, decimal pedidoTotalDesconto, string mensagem)
        {
            if (totalDescontoAplicado != pedidoTotalDesconto)
                throw new ExcecaoDominio(mensagem);
        }
    }
}
