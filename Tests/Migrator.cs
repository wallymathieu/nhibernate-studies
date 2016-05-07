using System;
using System.IO;
using NUnit.Framework;
using System.Linq;
using System.Data;
using FluentMigrator.Runner.ConnectionExecutor;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Announcers;

namespace SomeBasicNHApp.Tests
{

    public class Migrator
    {
        private readonly ConsoleAnnouncer consoleAnnouncer = new ConsoleAnnouncer();

        private readonly string _db;
        public Migrator(string db)
        {
            _db = db;
        }
        public void Migrate(IDbConnection conn)
        {
            var executor = new ConnectionTaskExecutor(new RunnerContext(consoleAnnouncer)
            {
                Database = "sqlite",
                Connection = "Data Source=" + _db + ";Version=3;",
                Targets = new[] { "DbMigrations.dll" }
            }, conn);
            executor.Execute();
        }
    }

}
