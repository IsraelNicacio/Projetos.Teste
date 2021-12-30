using DIP.Intefaces;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace DIP.Persistencia
{
    public class FirebirdConnection : IDBConnection
    {
        public FbConnection Connection { get; set; }
        public FbTransaction Transaction { get; set; }

        private readonly IConfiguration _configuration;

        public FirebirdConnection()
        {
            _configuration.GetConnectionString("mysqlDB");
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
                    this.Connection = new FbConnection("");
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
            FbCommand Command;
            FbParameter parameter;

            Command = new FbCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new FbParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            Command.ExecuteNonQuery();
        }

        public object ExecuteScalar(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            FbCommand Command;
            FbParameter parameter;

            Command = new FbCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new FbParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            return Command.ExecuteScalar();
        }

        public DataTable ExecuteToDataTable(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            FbCommand Command;
            FbParameter parameter;
            FbDataAdapter dataAdapter;

            Command = new FbCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new FbParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            //Executa
            dataAdapter = new FbDataAdapter(Command);
            //Carrega resultado
            DataTable objDataTable = new DataTable();
            dataAdapter.Fill(objDataTable);
            //Retorna
            return objDataTable;
        }

        public IDataReader ExecuteToDataReader(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            FbCommand Command;
            FbParameter parameter;
            FbDataAdapter dataAdapter;

            Command = new FbCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new FbParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            //Retorna
            return Command.ExecuteReader();
        }

        public List<IDataReader> ExecuteToListDataReader(SqlCommand sqlCommand, IEnumerable<SqlParameter> sqlParameters)
        {
            //Variavel local
            FbCommand Command;
            FbParameter parameter;
            FbDataAdapter dataAdapter;

            Command = new FbCommand(sqlCommand.CommandText, Connection, Transaction);

            foreach (var item in sqlParameters)
            {
                parameter = new FbParameter(item.ParameterName, item.Value);
                parameter.DbType = item.DbType;
                parameter.IsNullable = item.IsNullable;
                Command.Parameters.Add(parameter);
            }

            List<IDataReader> listDataReader = new List<IDataReader>();

            using (FbDataReader dataReader = Command.ExecuteReader())
            { 
                while (dataReader.Read())
                listDataReader.Add(dataReader);

                dataReader.Close();
            }

            //Retorna
            return listDataReader;
        }
    }
}
