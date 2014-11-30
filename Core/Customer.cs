namespace SomeBasicNHApp.Core
{
    public class Customer : IIdentifiableByNumber
    {

        public virtual int Id { get; set; }

        public virtual string Firstname { get; set; }

        public virtual string Lastname { get; set; }


        //public virtual Name Name
        //{
        //    get { return _name; }
        //    set
        //    {
        //        _name = value;
        //    }
        //}

        public virtual System.Collections.Generic.IList<Order> Orders { get; set; }

        public virtual int Version { get; set; }

    }
}
