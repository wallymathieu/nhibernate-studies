using SomeBasicNHApp.Core;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace SomeBasicNHApp.Core
{
    public class Session
    {
        private readonly IMapPath _mapPath;

        public Session(IMapPath mapPath)
        {
            _mapPath = mapPath;
        }
        private string WebPath()
        {
            var path= Directory.GetParent (_mapPath.MapPath(@"~/")).Parent;
            return Path.Combine(path.FullName ,".db.sqlite" );
        }
        public ISessionFactory CreateWebSessionFactory()
        {
            var file = WebPath();
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard
                  .UsingFile(file))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Customer>())
              .BuildSessionFactory();
        }
        public ISessionFactory CreateTestSessionFactory(string file)
        {
            return Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard.UsingFile(file))//NOTE:why not use in memory? some queries wont work for nhibernate
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Customer>())
              .ExposeConfiguration(cfg =>
                  new SchemaExport(cfg).Execute(true, true, false)
              )
              .BuildSessionFactory();
        }
    }
}
