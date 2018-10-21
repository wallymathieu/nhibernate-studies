module SomeBasicNHApp.Core.Session
open SomeBasicNHApp.Core.Entities
open NHibernate
open NHibernate.Tool.hbm2ddl
open FluentNHibernate.Automapping
open FluentNHibernate.Conventions
open FluentNHibernate.Conventions.Instances
open FluentNHibernate.Cfg
open FluentNHibernate.Cfg.Db

type TableNameConvention()=
    interface IClassConvention with
        member __.Apply(instance:IClassInstance) =
            instance.Table(instance.EntityType.Name + "s")

let configure (conf:FluentConfiguration) :FluentConfiguration =
    conf.Mappings(fun m ->
                          let autom =AutoPersistenceModel()
                                        .AddEntityAssembly(typeof<Customer>.Assembly)
                                        .Conventions.Add(TableNameConvention())
                                        .Where(fun t -> t.Namespace.EndsWith("SomeBasicNHApp.Core.Entities"))
                          m.AutoMappings.Add(autom) |> ignore
                          )
                          

let createSessionFactory (file:string) (newDb)=
            configure( Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile file ) )
                .ExposeConfiguration(fun cfg ->
                    // Hack from: http://www.caribousoftware.com/bobsblog/archive/2009/10/13/f-fluent-nhibernate-poco-class-object.aspx
                    cfg.Properties.Add ("use_proxy_validator", "false")
                    // f# generates internal fields that the proxy validator picks up
                    if (newDb) then SchemaExport(cfg).Execute(true, true, false))
                .BuildSessionFactory(); 
