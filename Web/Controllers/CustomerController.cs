using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using SomeBasicNHApp.Core.Entities;

namespace SomeBasicNHApp.Controllers
{
    public class CustomerController : Controller
    {
        private ISession _session;

        public CustomerController(ISession session)
        {
            _session = session;
        }
        //
        // GET: /Context/

        public ActionResult Index()
        {
            var contexts = from context in _session.Query<Customer>()
                select context;
            return View(contexts.ToArray());
        }

        public ActionResult Get(int id)
        {
            return View(_session.Get<Customer>(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [ActionName("Create"), AcceptVerbs("POST")]
        public ActionResult CreatePost(Customer context)
        {
            _session.Save(context);
            return Redirect("/Customer");
        }
    }
}
