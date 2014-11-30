using System;
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
using System.Text;

namespace SomeBasicNHApp.Tests
{

    [TestFixture]
    public class MigrationsTest
    {
        private UnityContainer _unityContainer;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            if (File.Exists("MigrationsTest.db")) { File.Delete("MigrationsTest.db"); }

            _unityContainer = new UnityContainer().RegisterCore(Runtime.Console);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            if (File.Exists("MigrationsTest.db")) { File.Delete("MigrationsTest.db"); }
        }
        
        [SetUp]
        public void Setup()
        {
        }

        class ExecuteAndRedirectOutput
        {
            private System.Diagnostics.Process _p;
            public ExecuteAndRedirectOutput(string file, string arguments)
            {
                _p = new System.Diagnostics.Process();
                _p.StartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = file,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                };
            }

            public void StartAndWaitForExit()
            {
                _p.Start();
                _p.WaitForExit();
                if (_p.ExitCode != 0)
                {
                    throw new Exception(String.Format("Process exit code {0}, with output:\n------\n{1}\n------\n", _p.ExitCode, _p.StandardError.ReadToEnd()));
                }
            }
        }

        [Test]
        public void Migrate()
        {
            var migrator = new ExecuteAndRedirectOutput(Path.Combine("..", "..", "..", "packages", "FluentMigrator.1.3.1.0", "tools", "Migrate.exe"), "/connection \"Data Source=MigrationsTest.db;Version=3;\" /db sqlite /target DbMigrations.dll");

            migrator.StartAndWaitForExit();
        }
    }
}
