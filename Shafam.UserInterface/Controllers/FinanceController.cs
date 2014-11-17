using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Finance)]
    public class FinanceController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Bills");
        }

        public ActionResult Bills()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }
	}
}