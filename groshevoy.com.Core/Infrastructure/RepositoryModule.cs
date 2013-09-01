using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using groshevoy.com.Core.Context;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace groshevoy.com.Core.Infrastructure
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<BlogContext>().ToSelf().InRequestScope();
            //.ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
            //.InRequestScope();
        }
    }
}
