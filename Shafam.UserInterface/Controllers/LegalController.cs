using System.Web.Mvc;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface.Controllers
{
    public class LegalController : Controller
    {
        [Authorize(Roles = UserRoles.Legal)]
        public ActionResult Index()
        {
            return RedirectToAction("Complaints");
        }

        public ActionResult Complaints()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }
	}
}