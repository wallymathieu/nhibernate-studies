using FluentMigrator;
using System;

namespace SomeBasicNHApp.DbMigrations
{
	[Migration(20150221221142)]
	public class RemoveNotNull_CustomerId :Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			Alter.Table("Orders").AlterColumn("Customer_id").AsInt32().Nullable();
		}

	}
}
