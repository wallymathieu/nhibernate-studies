using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;

namespace SomeBasicNHApp.Code
{
    public class SessionController : Controller
    {
        public HttpSessionStateBase HttpSession
        {
            get { return base.Session; }
        }

        public new ISession Session
        {
            get
            {
                return (ISession)this.HttpContext.Items[NHibernateActionFilter.Name];
            }
        }
    }
}