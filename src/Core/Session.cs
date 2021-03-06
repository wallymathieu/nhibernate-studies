﻿using SomeBasicNHApp.Core;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;
using System;
using SomeBasicNHApp.Core.Entities;

namespace SomeBasicNHApp.Core
{
    public class Session
    {
        public class TableNameConvention : IClassConvention
        {
            public void Apply(FluentNHibernate.Conventions.Instances.IClassInstance instance)
            {
                string typeName = instance.EntityType.Name;

                instance.Table(typeName + "s");
            }
        }
        private readonly IMapPath _mapPath;

        public Session(IMapPath mapPath) => _mapPath = mapPath;
        
        private string WebPath() => _mapPath.MapPath(".db.sqlite");

        private FluentConfiguration ConfigureMaps(FluentConfiguration conf)
        {
#if CLASSMAP
			return conf.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Customer>());
#else
            return conf.Mappings(m => m.AutoMappings.Add(new AutoPersistenceModel()
                .AddEntityAssembly(typeof(Customer).Assembly)
                .Conventions.Add(new TableNameConvention())
                .Where(t => t.Namespace.EndsWith("Core.Entities")))
                );
#endif
        }

        public ISessionFactory CreateWebSessionFactory() => 
            ConfigureMaps(Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(WebPath()))
            ).BuildSessionFactory();

        public ISessionFactory CreateTestSessionFactory(string file, bool newDb = false)
        {
            return ConfigureMaps(Fluently.Configure()
              .Database(
                SQLiteConfiguration.Standard.UsingFile(file))//NOTE:why not use in memory? some queries wont work for nhibernate
              ).ExposeConfiguration(cfg =>
              {
                  if (newDb)
                  {
                      new SchemaExport(cfg).Execute(true, true, false);
                  }
              })
              .BuildSessionFactory();
        }
    }
}
