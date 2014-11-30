using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Cfg;
using SomeBasicNHApp.Core;

namespace SomeBasicNHApp.Code
{
    public class NHibernateActionFilter : ActionFilterAttribute
    {
        private static readonly ISessionFactory sessionFactory = BuildSessionFactory();
        public const string Name = "NHibernateSession";

        private static ISessionFactory BuildSessionFactory()
        {
            return new Session(new WebMapPath()).CreateWebSessionFactory();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items[Name] = sessionFactory.OpenSession();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = (ISession)filterContext.HttpContext.Items[Name];
            if (session != null)
            {
                session.Dispose();
            }
        }
    }
}