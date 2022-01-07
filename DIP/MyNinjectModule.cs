using DIP.Intefaces;
using DIP.Persistencia;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP
{
    public class MyNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDBConnection>().To<FirebirdConnection>();
        }
    }
}
