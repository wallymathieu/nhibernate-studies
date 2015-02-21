using System;
using System.IO;
using NUnit.Framework;

namespace SomeBasicNHApp.Tests
{

	[TestFixture]
    public class MigrationsTest
    {

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            if (File.Exists("MigrationsTest.db")) { File.Delete("MigrationsTest.db"); }

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

        public class ExecuteAndRedirectOutput
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
