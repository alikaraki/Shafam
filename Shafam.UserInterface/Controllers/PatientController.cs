using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.UserInterface.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        //
        // GET: /Patient/
        public ActionResult Index()
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

            return RedirectToAction("Index");
        }

        public ActionResult Profile(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            return View(patient);
        }

        public ActionResult Visitations(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            return View(patient);
        }

        public ActionResult Medications(int patientId)
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