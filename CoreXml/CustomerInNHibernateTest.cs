using System.Data.SQLite;
using NHibernate;
using NUnit.Framework;
using System.IO;
using SomeBasicNHApp.Core.Entities;

namespace SomeBasicNHApp.CoreXml
{
    [TestFixture]
    public class CustomerInNHibernateTest
    {
        private NHibernate.ISession _session;

        private ISessionFactory _sessionManager;


        [SetUp]
        public void Setup()
        {
            if (File.Exists("CustomerInNHibernateTest.db")) { File.Delete("CustomerInNHibernateTest.db"); }

            var cfg = new NHibernate.Cfg.Configuration();

            cfg.Configure();

            var schema = new NHibernate.Tool.hbm2ddl.SchemaExport(cfg);
            using (var connection = new SQLiteConnection(@"Data Source=CustomerInNHibernateTest.db;Version=3;New=True"))
            {
                connection.Open();
                schema.Execute(true, true, false, connection, null);
            }
            _sessionManager = cfg.BuildSessionFactory();

            _session = _sessionManager.OpenSession();
        }

        [TearDown]
        public void TearDown()
        {
        }


        [Test]
        public void CanAddCustomer()
        {
            var customer = new Customer { Firstname = "Steve", Lastname = "Bohlen" };
            var newIdentity = _session.Save(customer);


            var testCustomer = _session.Get<Customer>(newIdentity);

            Assert.IsNotNull(testCustomer);
        }

    }
}
