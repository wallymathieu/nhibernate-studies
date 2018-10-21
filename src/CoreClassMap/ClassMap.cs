using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using SomeBasicNHApp.Core.Entities;

namespace SomeBasicNHApp.Core
{

    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id);
            Map(x => x.Firstname);
            Map(x => x.Lastname);
            Map(x => x.Version);
            HasMany(x => x.Orders).Table("Orders");
            Table("Customers");
        }
    }

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.Id);
            Map(x => x.OrderDate);
            Map(x => x.Version);
            References(x => x.Customer).Column("Customer_id");
            HasManyToMany(x => x.Products).Table("OrdersToProducts")
                .ChildKeyColumn("Order_id")
                .ParentKeyColumn("Product_id");
            Table("Orders");
        }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id);
            Map(x => x.Cost);
            Map(x => x.Version);
            Map(x => x.Name);
            HasManyToMany(x => x.Orders).Table("OrdersToProducts")
                .ChildKeyColumn("Product_id")
                .ParentKeyColumn("Order_id");
            Table("Products");
        }
    }

}
