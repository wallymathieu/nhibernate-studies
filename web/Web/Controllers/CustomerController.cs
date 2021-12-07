using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using SomeBasicNHApp.Core.Entities;
using SomeBasicNHApp.Models;

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

        public ActionResult Get(int id) =>
            View(_session.Get<Customer>(id));

        [ActionName("Create"), HttpGet]
        public ActionResult Create() => 
            View();

        [ActionName("Create"), HttpPost]
        public ActionResult CreatePost([Bind("Firstname","Lastname")]Customer context)
        {
            _session.Save(context);
            return Redirect("/Customer");
        }

        [ActionName("Edit"), HttpGet]
        public IActionResult Edit([FromRoute]int id) =>
            View(_session.Get<Customer>(id));
        
        [ActionName("Edit"), HttpPost]
        public ActionResult EditPost([FromQuery]int id, CustomerEditModel model)
        {
            var customer = _session.Get<Customer>(id);
            customer.Firstname = model.Firstname;
            customer.Lastname = model.Lastname;
            return Redirect("/Customer");
        }


        public IActionResult Details(int id)=>
            View(_session.Get<Customer>(id));

        public IActionResult Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
