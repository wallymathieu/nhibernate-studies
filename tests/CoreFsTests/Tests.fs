module Tests

open Xunit
open FSharp.Data
open SomeBasicNHApp.Core
open SomeBasicNHApp.Core.Entities
open System.Collections.Generic
open System.IO
open NHibernate

module TestData=
    type TestData = XmlProvider<"../Tests/TestData/TestData.xml", Global=false>

    let fillDb (sessionFactory:ISessionFactory)=
        use session = sessionFactory.OpenSession()
        use tnx = session.BeginTransaction()

        let toCustomer (o : TestData.Customer) =
            Customer.Create(id=o.Id, version = o.Version, firstname = o.Firstname, lastname = o.Lastname,
                            orders = List<_>())

        let toOrder (o : TestData.Order)=
            Order.Create(id = o.Id, version = o.Version, customer = session.Get<Customer>(o.Customer), orderDate = o.OrderDate.DateTime,
                         products = List<_>() )

        let toProduct (o : TestData.Product)=
            Product.Create(id=o.Id, version=o.Version, name=o.Name, cost=float o.Cost,
                           products=List<_>())

        let toOrderProduct(o : TestData.OrderProduct)=
            let order=session.Get<Order>(o.Order)
            let product=session.Get<Product>(o.Product)
            (order,product)

        use f = File.Open("TestData/TestData.xml", FileMode.Open, FileAccess.Read, FileShare.Read)
        let db = TestData.Load(f)

        for customer in db.Customers |> Array.map toCustomer do
            session.Save customer |> ignore
        
        for order in db.Orders |> Array.map toOrder do
            session.Save order |> ignore
        for product in db.Products |> Array.map toProduct do
            session.Save product |> ignore
        for order,product in db.OrderProducts |> Array.map toOrderProduct do
            order.Products.Add product
        tnx.Commit()

type CustomerDataTests()=
    let mutable sessionFactory =null
    let mutable session=null
    do
        if File.Exists "CustomerDataTests.db" then
            File.Delete "CustomerDataTests.db"
            
        sessionFactory<- Session.createSessionFactory "CustomerDataTests.db" true
        TestData.fillDb sessionFactory
        session<-sessionFactory.OpenSession()

    [<Fact>]
    member this.CanGetCustomerById()=
        Assert.NotNull(session.Get<Customer>(1))

    [<Fact>]
    member this.CanGetProductById()=
        Assert.NotNull(session.Get<Product>(1))

    [<Fact>]
    member this.OrderContainsProduct()=
        let order = session.Get<Order>(1)
        Assert.True(order.Products |> Seq.tryFind( fun p -> p.Id = 1) |> Option.isSome)


