using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransfer
{
    public class CustomerFirstnameCounter
    {
        private string _firstname;
        public string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
            }
        }

        private long _count;
        public long Count
        {
            get { return _count; }
            set
            {
                _count = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CustomerFirstnameCounter class.
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="count"></param>
        public CustomerFirstnameCounter(string firstname, long count)
        {
            _firstname = firstname;
            _count = count;
        }

    }
}
