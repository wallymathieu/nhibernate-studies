using System.IO;
using System.Xml.Linq;
using SomeBasicNHApp.Core;
using NHibernate;
using Order = SomeBasicNHApp.Core.Entities.Order;
using System.Linq;
using SomeBasicNHApp.Core.Entities;
using System;
using Xunit;

namespace SomeBasicNHApp.Tests
{
    public class CustomerDataTests:IDisposable
    {

        private static ISessionFactory _sessionFactory;

        private ISession _session;


        [Fact]
        public void CanGetCustomerById()
        {
            var customer = _session.Get<Customer>(1);

            Assert.NotNull(customer);
        }

        [Fact]
        public void CustomerHasOrders()
        {
            var customer = _session.Get<Customer>(1);

            Assert.True(customer.Orders.Any());
        }

        [Fact]
        public void ProductsArePartOfOrders()
        {
            var product = _session.Get<Product>(1);

            Assert.True(product.Orders.Any());
        }

        [Fact]
        public void CanGetCustomerByFirstname()
        {
            var customers = _session.QueryOver<Customer>()
                .Where(c => c.Firstname == "Steve")
                .List<Customer>();
            Assert.Equal(3, customers.Count);
        }

        [Fact]
        public void CanGetProductById()
        {
            var product = _session.Get<Product>(1);

            Assert.NotNull(product);
        }
        [Fact]
        public void OrderContainsProduct()
        {
            Assert.True(_session.Get<Order>(1).Products.Any(p => p.Id == 1));
        }
        [Fact]
        public void OrderHasACustomer()
        {
            Assert.False(string.IsNullOrWhiteSpace( _session.Get<Order>(1).Customer.Firstname));
        }


        public CustomerDataTests()
        {
            _session = _sessionFactory.OpenSession();
        }


        public void Dispose()
        {
            _session.Close();
        }

        static CustomerDataTests()
        {
            if (File.Exists("CustomerDataTests.db")) { File.Delete("CustomerDataTests.db"); }

            _sessionFactory = new Session(new ConsoleMapPath()).CreateTestSessionFactory("CustomerDataTests.db");
            var doc = XDocument.Load(Path.Combine("TestData", "TestData.xml"));
            var import = new XmlImport(doc, "http://tempuri.org/Database.xsd");
            using (var session = _sessionFactory.OpenSession())
            {
                new Migrator("CustomerDataTests.db").Migrate(session.Connection);
            }
            using (var session = _sessionFactory.OpenSession())
            using (var tnx = session.BeginTransaction())
            {
                import.Parse(new[] { typeof(Customer), typeof(Order), typeof(Product) },
                                (type, obj) => session.Save(type.Name, obj), onIgnore: (type, property) =>
                                {
                                    Console.WriteLine("ignoring property {1} on {0}", type.Name, property.PropertyType.Name);
                                });
                tnx.Commit();
            }
            using (var session = _sessionFactory.OpenSession())
            using (var tnx = session.BeginTransaction())
            {
                import.ParseConnections("OrderProduct", "Product", "Order", (productId, orderId) =>
                {
                    var product = session.Get<Product>(productId);
                    var order = session.Get<Order>(orderId);
                    order.Products.Add(product);
                });

                import.ParseIntProperty("Order", "Customer", (orderId, customerId) =>
                {
                    session.Get<Order>(orderId).Customer = session.Get<Customer>(customerId);
                });
                tnx.Commit();
            }
        }
    }
}
