using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP.Intefaces
{
    public interface IDBConnection : IDisposable, ICRUD
    {
        public void OpenConnection();
        public void CloseConnection();
        public void BeginTransaction();
        public void CommitTransaction();
        public void RollBackTransaction();
    }
}
