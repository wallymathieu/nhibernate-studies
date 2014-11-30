using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransfer
{
    public class YoYoCustomer
    {
        #region Fields

        private int _customerId;

        private string _firstname;

        private string _lastname;

        private DateTime _orderDate;

        private int _orderId;

        #endregion

        #region Properties

        public virtual int CustomerId
        {
            get { return _customerId; }
            private set
            {
                _customerId = value;
            }
        }

        public virtual string Firstname
        {
            get { return _firstname; }
            private set
            {
                _firstname = value;
            }
        }

        public virtual string Lastname
        {
            get { return _lastname; }
            private set
            {
                _lastname = value;
            }
        }

        public virtual DateTime OrderDate
        {
            get { return _orderDate; }
            private set
            {
                _orderDate = value;
            }
        }

        public virtual int OrderId
        {
            get { return _orderId; }
            private set
            {
                _orderId = value;
            }
        }

        #endregion

    }
}
