using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransfer
{
    public class PreferredCustomer : Customer
    {
        #region Fields

        private DateTime _customerSince;

        private float _orderDiscountRate;

        #endregion

        #region Properties

        public virtual DateTime CustomerSince
        {
            get { return _customerSince; }
            set
            {
                _customerSince = value;
            }
        }

        public virtual float OrderDiscountRate
        {
            get { return _orderDiscountRate; }
            set
            {
                _orderDiscountRate = value;
            }
        }

        #endregion

    }
}
