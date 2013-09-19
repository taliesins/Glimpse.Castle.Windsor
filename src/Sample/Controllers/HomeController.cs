using System.Web.Mvc;
using Sample.Components;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        private IComponent2 component2;
        private IComponent4 _component4;

        public HomeController(IComponent2 component2, IComponent4 component4)
        {
            this.component2 = component2;
            _component4 = component4;
        }

        public ActionResult Index()
        {
            return View();
        }
        
    }
}