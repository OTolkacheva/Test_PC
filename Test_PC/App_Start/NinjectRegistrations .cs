using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test_PC.Db;

namespace Test_PC.App_Start
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<DbReposytory>();
        }
    }
}