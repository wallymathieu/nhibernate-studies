using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTransfer
{
    public class Name
    {
        #region Fields

        private string _firstname;

        private string _lastname;

        #endregion

        #region Properties

        public string Firstname
        {
            get { return _firstname; }
            set
            {
                _firstname = value;
            }
        }

        public string Fullname
        {
            get { return _firstname + " " + _lastname; }

        }

        public string Lastname
        {
            get { return _lastname; }
            set
            {
                _lastname = value;
            }
        }

        #endregion

    }
}
