using System;
using NHibernate;
using System.IO;
using SomeBasicNHApp.Core.Entities;
using SomeBasicNHApp.Core;
using Xunit;

namespace SomeBasicNHApp.CoreXml
{
	public class CustomerInNHibernateTest:IDisposable
	{
		private NHibernate.ISession _session;

		private ISessionFactory _sessionManager;


		public CustomerInNHibernateTest()
		{
			if (File.Exists("CustomerInNHibernateTest.db")) { File.Delete("CustomerInNHibernateTest.db"); }

			_sessionManager = new Session(new ConsoleMapPath()).CreateTestSessionFactory("CustomerInNHibernateTest.db");
			_session = _sessionManager.OpenSession();
		}

		public void Dispose()
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


		[Fact]
		public void CanAddCustomer()
		{
			var customer = new Customer { Firstname = "Steve", Lastname = "Bohlen" };
			var newIdentity = _session.Save(customer);


			var testCustomer = _session.Get<Customer>(newIdentity);

			Assert.NotNull(testCustomer);
		}

	}
}
