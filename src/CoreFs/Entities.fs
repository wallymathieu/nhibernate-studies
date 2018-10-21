namespace SomeBasicNHApp.Core.Entities
open System
open System.Collections.Generic
(*
type Order = {mutable Id:int;mutable OrderDate:DateTime;mutable Customer:Customer;Products:List<Product>;mutable Version:int }
and Customer= {mutable Id:int;mutable Firstname:string;mutable Lastname:string;Orders:List<Order>;mutable Version:int}
and Product= {mutable Id:int;mutable Cost:float;mutable Name:string;Products: List<Product>;mutable Version:int}
*)

type [<AllowNullLiteral>] Order(id,orderDate,customer,version) =
    new()=Order(0,DateTime.MinValue,null,0)
    abstract Id : int with get, set
    default val Id =id with get, set
    
    abstract Version : int with get, set
    default val Version =version with get, set
    
    abstract OrderDate : DateTime with get, set
    default val OrderDate =orderDate with get, set
    
    abstract Customer : Customer with get, set
    default val Customer : Customer=customer with get, set
    
    abstract Products : IList<Product> with get, set
    default val Products =List<Product>() :> IList<_> with get, set

and [<AllowNullLiteral>] Customer(id,firstname,lastname,version)=
    new()=Customer(0,"","",0)
    abstract Id : int with get, set
    default val Id =id with get, set
    
    abstract Version : int with get, set
    default val Version =version with get, set
    
    abstract Firstname : string with get, set
    default val Firstname =firstname with get, set
    
    abstract Lastname : string with get, set
    default val Lastname =lastname with get, set

    abstract Orders : IList<Order> with get, set
    default val Orders = List<Order>() :>IList<_> with get, set
and [<AllowNullLiteral>] Product(id,cost,name,version)=
    new()=Product(0,0.0,"",0)
    abstract Id : int with get, set
    default val Id =id with get, set
    
    abstract Version : int with get, set
    default val Version =version with get, set

    abstract Cost : float with get, set
    default val Cost =cost with get, set
    
    abstract Name : string with get, set
    default val Name = name with get, set
    
    abstract Products : IList<Product> with get, set    
    default val Products = List<Product>() :>IList<_> with get, set


