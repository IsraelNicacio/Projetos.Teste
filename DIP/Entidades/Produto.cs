using DIP.Intefaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP.Entidades
{
    public class Produto
    {
        private readonly IDBConnection dBConnection;

        public Produto(IDBConnection dBConnection)
        {
            this.dBConnection = dBConnection;
        }

        public void Recuperar()
        {
            try
            {
                SqlDataAdapter dataAdapter;
                SqlCommand command;

                this.dBConnection.OpenConnection();

                command = new SqlCommand(@"SELECT * PRODUTO");


            }
            catch (Exception)
            {
                this.dBConnection.RollBackTransaction();
            }
        }
    }
}
