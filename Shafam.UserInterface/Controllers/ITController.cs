using System.Web.Mvc;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.IT)]
    public class ITController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Users");
        }
        
        public ActionResult Users()
        {
            return View();
        }
	}
}