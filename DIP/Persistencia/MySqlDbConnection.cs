using DIP.Intefaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

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

        public void ExecuteNonQuery(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            MySqlCommand Command;
            MySqlParameter parameter;

            Command = new MySqlCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new MySqlParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            Command.ExecuteNonQuery();
        }

        public object ExecuteScalar(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            MySqlCommand Command;
            MySqlParameter parameter;

            Command = new MySqlCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new MySqlParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            return Command.ExecuteScalar();
        }

        public DataTable ExecuteToDataTable(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            MySqlCommand Command;
            MySqlParameter parameter;
            MySqlDataAdapter dataAdapter;

            Command = new MySqlCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new MySqlParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            //Executa
            dataAdapter = new MySqlDataAdapter(Command);
            //Carrega resultado
            DataTable objDataTable = new DataTable();
            dataAdapter.Fill(objDataTable);
            //Retorna
            return objDataTable;
        }

        public IDataReader ExecuteToDataReader(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            MySqlCommand Command;
            MySqlParameter parameter;

            Command = new MySqlCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new MySqlParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            MySqlDataReader dataReader = Command.ExecuteReader();
            //Retorna
            return dataReader;
        }
    }
}
