using DIP.Intefaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace DIP.Persistencia
{
    public class MySqlDbConnection : IDBConnection
    {
        public MySqlConnection Connection { get; set; }
        public MySqlTransaction Transaction { get; set; }
        
        private readonly IConfiguration _configuration;

        public MySqlDbConnection()
        {
            _configuration.GetConnectionString("firebirdDB");
        }

        public void BeginTransaction()
        {
            OpenConnection();
            this.Transaction = this.Connection.BeginTransaction(IsolationLevel.Serializable);
        }

        public void CloseConnection()
        {
            try
            {
                if (this.Connection != null)
                {
                    this.Connection.Close();
                    this.Connection = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao fechar a conexão ao banco de dados.", ex);
            }
        }

        public void CommitTransaction()
        {
            if (this.Transaction != null)
                this.Transaction.Commit();

            CloseConnection();
        }

        public void OpenConnection()
        {
            try
            {
                if (this.Connection == null)
                {
                    this.Connection = new MySqlConnection("");
                    this.Connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao abrir a conexão ao banco de dados.", ex);
            }
        }

        public void RollBackTransaction()
        {
            if (this.Transaction != null && this.Transaction.Connection != null)
                this.Transaction.Rollback();

            CloseConnection();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
