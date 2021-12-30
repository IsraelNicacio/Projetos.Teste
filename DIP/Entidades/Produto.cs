using DIP.Intefaces;
using System;
using System.Collections.Generic;
using System.Data;
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

                command = new SqlCommand(@"SELECT * FROM PRODUTO");
                DataTable dt = this.dBConnection.ExecuteToDataTable(command, new List<SqlParameter>());

            }
            catch (Exception)
            {
                this.dBConnection.RollBackTransaction();
            }
        }

        public void Recuperar2()
        {
            try
            {
                SqlDataAdapter dataAdapter;
                SqlCommand command;

                this.dBConnection.OpenConnection();

                command = new SqlCommand(@"SELECT * FROM PRODUTO");
                IDataReader dr = this.dBConnection.ExecuteToDataReader(command, new List<SqlParameter>());
                while (dr.Read())
                {
                    var codigo = dr["CPROD"].ToString();
                    var Descricao = dr["XPROD"].ToString();
                }
            }
            catch (Exception)
            {
                this.dBConnection.RollBackTransaction();
            }
        }
    }
}
