namespace SomeBasicNHApp.Core.Entities
open System
open System.Collections.Generic

type [<AllowNullLiteral>] Order(id,orderDate,customer,version) =
    new()=Order(0,DateTime.MinValue,null,0)
    member val Id =id with get, set
    
    member val Version =version with get, set
    
    member val OrderDate =orderDate with get, set
    
    member val Customer : Customer=customer with get, set
    
    member val Products =List<Product>() :> IList<_> with get, set

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


