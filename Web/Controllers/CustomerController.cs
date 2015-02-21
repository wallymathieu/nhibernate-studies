using System.Linq;
using System.Web.Mvc;
using SomeBasicNHApp.Code;
using SomeBasicNHApp.Core;
using NHibernate.Linq;
using SomeBasicNHApp.Core.Entities;

namespace SomeBasicNHApp.Controllers
{
  public class CustomerController : SessionController
  {
    //
    // GET: /Context/

    public ActionResult Index()
    {
      var contexts = from context in Session.Query<Customer>()
                       select context;
      return View(contexts.ToArray());
    }
    public ActionResult Get(int id)
    {
       return View(Session.Get<Customer>(id));
    }
    public ActionResult Create()
    {
      return View();
    }
    [ActionName("Create"),AcceptVerbs(HttpVerbs.Post)]
    public ActionResult CreatePost(Customer context)
    {
        Session.Save(context);
        return Redirect("/Customer");
    }
  }
}
