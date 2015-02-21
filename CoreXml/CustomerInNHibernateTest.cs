using System.Data.SQLite;
using NHibernate;
using NUnit.Framework;
using System.IO;
using SomeBasicNHApp.Core.Entities;
using SomeBasicNHApp.Core;

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

			_sessionManager = new Session(new ConsoleMapPath()).CreateTestSessionFactory("CustomerInNHibernateTest.db");
			_session = _sessionManager.OpenSession();
		}

		[TearDown]
		public void TearDown()
		{
			if (_session != null)
			{
				_session.Dispose();
				_session = null;
			}
			if (_sessionManager != null)
			{
				_sessionManager.Dispose();
				_sessionManager = null;
			}
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
