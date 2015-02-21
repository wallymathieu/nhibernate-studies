using SomeBasicNHApp.Core;
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
		private class CustomConf : DefaultAutomappingConfiguration
		{
			public override bool IsId(Member member)
			{
				return member.Name.Equals("Id", StringComparison.InvariantCultureIgnoreCase);
			}
		}

		private readonly IMapPath _mapPath;

		public Session(IMapPath mapPath)
		{
			_mapPath = mapPath;
		}
		private string WebPath()
		{
			var path = Directory.GetParent(_mapPath.MapPath(@"~/")).Parent;
			return Path.Combine(path.FullName, ".db.sqlite");
		}
		private FluentConfiguration ConfigureMaps(FluentConfiguration conf)
		{
#if CLASSMAP
			return conf.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Customer>());
#else
			return conf.Mappings(m => m.AutoMappings.Add(new AutoPersistenceModel()
				.AddEntityAssembly(typeof(Customer).Assembly)
				.Where(t => t.Namespace.EndsWith("Core.Entities")))
				);
#endif
		}

		public ISessionFactory CreateWebSessionFactory()
		{

			var file = WebPath();
			return ConfigureMaps(Fluently.Configure()
			  .Database(
				SQLiteConfiguration.Standard
				  .UsingFile(file)))
			  .BuildSessionFactory();
		}
		public ISessionFactory CreateTestSessionFactory(string file)
		{
			return ConfigureMaps(Fluently.Configure()
			  .Database(
				SQLiteConfiguration.Standard.UsingFile(file))//NOTE:why not use in memory? some queries wont work for nhibernate
			  ).ExposeConfiguration(cfg =>
				  new SchemaExport(cfg).Execute(true, true, false)
			  )
			  .BuildSessionFactory();
		}
	}
}
