﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using SomeBasicNHApp.Core;
using SomeBasicNHApp.DbMigrations;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using Order = SomeBasicNHApp.Core.Order;
using System.Linq;
namespace SomeBasicNHApp.Tests
{
    [TestFixture]
    public class CustomerDataTests
    {

        private ISessionFactory _sessionFactory;

        private ISession _session;
        private IUnityContainer _unityContainer;


        [Test]
        public void CanGetCustomerById()
        {
            var customer = _session.Get<Customer>(1);

            Assert.IsNotNull(customer);
        }

        [Test]
        public void CanGetCustomerByFirstname()
        {
            var customers = _session.QueryOver<Customer>()
                .Where(c => c.Firstname == "Steve")
                .List<Customer>();
            Assert.AreEqual(3, customers.Count);
        }

        [Test]
        public void CanGetProductById()
        {
            var product = _session.Get<Product>(1);

            Assert.IsNotNull(product);
        }
		[Test]
		public void OrderContainsProduct()
		{
			Assert.True(_session.Get<Order>(1).Products.Any(p => p.Id == 1));
		}
		[Test]
		public void OrderHasACustomer()
		{
			Assert.IsNotNullOrEmpty(_session.Get<Order>(1).Customer.Firstname);
		}


		[SetUp]
        public void Setup()
        {
            _session = _sessionFactory.OpenSession();
        }


        [TearDown]
        public void TearDown()
        {
            _session.Close();
        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _unityContainer = new UnityContainer().RegisterCore(Runtime.Console);
            if (File.Exists("CustomerDataTests.db")) { File.Delete("CustomerDataTests.db"); }

            _sessionFactory = _unityContainer.Resolve<Session>().CreateTestSessionFactory("CustomerDataTests.db");
			var doc = XDocument.Load(Path.Combine("TestData", "TestData.xml"));
            using (var session = _sessionFactory.OpenSession())
            using (var tnx = session.BeginTransaction())
            {
                XmlImport.Parse(doc, new[] { typeof(Customer), typeof(Order), typeof(Product) },
                                (type, obj) => session.Save(type.Name, obj), "http://tempuri.org/Database.xsd");
                tnx.Commit();
            }
			using (var session = _sessionFactory.OpenSession())
			using (var tnx = session.BeginTransaction())
			{
				XmlImport.ParseConnections(doc, "OrderProduct", "Product", "Order", (productId, orderId) => {
					var product = session.Get<Product>(productId);
					var order = session.Get<Order>(orderId);
					order.Products.Add(product);
				}, "http://tempuri.org/Database.xsd");

				XmlImport.ParseIntProperty(doc,"Order","Customer",
				(orderId, customerId) => {
					session.Get<Order>(orderId).Customer = session.Get<Customer>(customerId);
				}, "http://tempuri.org/Database.xsd");
				tnx.Commit();
			}
		}

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _sessionFactory.Dispose();
        }
    }
}
