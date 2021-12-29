using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatearDesconto.Exceptions
{
    public class ExcecaoDominio : Exception
    {
        public ExcecaoDominio()
        {
        }

        public ExcecaoDominio(string mensagem) : base(mensagem)
        {
        }

        public ExcecaoDominio(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
        }
    }
}
