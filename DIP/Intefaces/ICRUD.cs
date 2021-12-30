using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP.Intefaces
{
    public interface ICRUD
    {
        public void ExecuteNonQuery(SqlCommand command, IEnumerable<SqlParameter> sqlParameters);
        public object ExecuteScalar(SqlCommand command, IEnumerable<SqlParameter> sqlParameters);
        public DataTable ExecuteToDataTable(SqlCommand command, IEnumerable<SqlParameter> sqlParameters);
    }
}
