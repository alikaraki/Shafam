using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Controllers
{
    public class VisitationController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public VisitationController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public ActionResult AddVisitation(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);

            return View(new VisitationViewModel { Patient = patient});
        }

        [HttpPost]
        public ActionResult AddVisitation(int patientId, VisitationViewModel viewModel)
        {
            // Add visitation record to repository
            // Redirect to patient details

            return RedirectToAction("PatientDetails", "Patient", new {patientId = patientId});
        }
	}
}