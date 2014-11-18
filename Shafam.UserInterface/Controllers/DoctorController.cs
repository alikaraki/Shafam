using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Shafam.BusinessLogic;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.UserInterface.Infrastructure;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Doctor)]
    public class DoctorController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientManagementService _patientManagementService;

        public DoctorController(IPatientRepository patientRepository,
                                IPatientManagementService patientManagementService)
        {
            _patientRepository = patientRepository;
            _patientManagementService = patientManagementService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            return View();
        }

        //
        // GET: /Patient/
        public ActionResult Patients()
        {
            IEnumerable<Patient> patients = _patientRepository.GetPatients();

            return View(patients);
        }

        public ActionResult AddRandomPatient()
        {
            int id = new Random().Next(100);

            var patient = new Patient
                          {
                              FirstName = "First Name " + id,
                              LastName = "Last Name " + id,
                              Age = id
                          };

            _patientRepository.AddPatient(patient);

            return RedirectToAction("Patients");
        }

        public ActionResult PatientProfile(int patientId)
        {
            Patient patient = _patientManagementService.ViewPatient(patientId);
            return View(patient);
        }

        public ActionResult Visitations(int patientId)
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

        [HttpGet]
        public ActionResult AddVisitation(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);

            return View(new VisitationViewModel { Patient = patient });
        }

        [HttpPost]
        public ActionResult AddVisitation(int patientId, VisitationViewModel viewModel)
        {
            // Add visitation record to repository
            // Redirect to patient details

            return RedirectToAction("PatientProfile", "Doctor", new { patientId = patientId });
        }
    }
}
