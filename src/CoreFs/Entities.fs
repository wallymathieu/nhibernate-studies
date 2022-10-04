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
    static member Create(id,version,orderDate,customer,products) = { Id = id; Version = version; OrderDate = orderDate; Customer = customer; Products =products }
and [<AllowNullLiteral>] Customer(id,firstname,lastname,version)=
    new()=Customer(0,"","",0)
    member val Id =id with get, set
    
    member val Version =version with get, set
    
    member val Firstname =firstname with get, set
    
    member val Lastname =lastname with get, set

    member val Orders = List<Order>() :>IList<_> with get, set
and [<AllowNullLiteral>] Product(id,cost,name,version)=
    new()=Product(0,0.0,"",0)
    member val Id =id with get, set
    
    member val Version =version with get, set

    member val Cost =cost with get, set
    
    member val Name = name with get, set
    
    member val Products = List<Product>() :>IList<_> with get, set


