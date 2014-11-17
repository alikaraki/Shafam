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
    [Authorize(Roles = UserRoles.Staff)]
    public class StaffController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public StaffController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Patients");
        }

        public ActionResult Patients()
        {
            IEnumerable<Patient> patients = _patientRepository.GetPatients();

            return View(patients);
        }

        public ActionResult PatientProfile(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            return View(patient);
        }

        public ActionResult Medication(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            return View(patient);
        }

        public ActionResult Treatments(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            return View(patient);
        }

        public ActionResult Tests(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            return View(patient);
        }
	}
}