using NHibernate;
using NHibernate.Cfg;
using System.Data.SQLite;

namespace SomeBasicNHApp.Core
{
    public class Session
    {
        private readonly IMapPath _mapPath;

        public Session(IMapPath mapPath)
        {
            _mapPath = mapPath;
        }
        public ISessionFactory CreateTestSessionFactory(string file, bool newDb = false)
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.SetProperty("connection.connection_string", "Data Source=" + file + ";Version=3");
            if (newDb)
            {
                var schema = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);
                using (var connection = new SQLiteConnection(@"Data Source=" + file + ";Version=3;New=True"))
                {
                    connection.Open();
                    schema.Execute(true, true, false, connection, null);
                }
            }
            return cfg.BuildSessionFactory();
        }
    }
}
