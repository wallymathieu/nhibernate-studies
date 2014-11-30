using System.Collections.Generic;

namespace CoreXml
{
    public class Product 
    {

        public virtual float Cost { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Order> Orders { get; set; }

        public virtual int Id { get; set; }

        public virtual int Version { get; set; }


    }
}
