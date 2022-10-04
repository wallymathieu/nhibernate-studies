namespace SomeBasicNHApp.Core.Entities
open System
open System.Collections.Generic

type [<CLIMutable>] Order = {
    Id: int
    Version: int
    OrderDate: DateTime
    Customer: Customer
    Products: IList<Product>
}
with
    static member Create(id,version,orderDate,customer,products)
        = { Id = id
            Version = version
            OrderDate = orderDate
            Customer = customer
            Products =products }
and [<CLIMutable>] Customer = {
    Id: int
    Version: int
    Firstname: string
    Lastname: string
    Orders: IList<Order>
}
with static member Create(id,firstname,lastname,version,orders)
        = { Id = id
            Firstname = firstname
            Lastname = lastname
            Version = version
            Orders = orders }
and [<CLIMutable>] Product = {
    Id: int
    Version: int
    Cost: float
    Name: string
    Products: IList<Product> }
with static member Create(id,cost,name,version,products)
        = { Id = id
            Version = version
            Cost = cost
            Name = name
            Products = products }

