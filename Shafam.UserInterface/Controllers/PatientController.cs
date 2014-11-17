using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Patient)]
    public class PatientController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Appointments");
        }

        public ActionResult Appointments()
        {
            return View();
        }

        public ActionResult Medication()
        {
            return View();
        }

        public ActionResult Tests()
        {
            return View();
        }
	}
}