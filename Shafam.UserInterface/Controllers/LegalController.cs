using System;
using System.Web.Mvc;
using Shafam.UserInterface.Infrastructure;
using Shafam.DataAccess;
using Shafam.BusinessLogic;
using Shafam.Common.DataModel;
using System.Collections.Generic;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Legal)]
    public class LegalController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IVisitationManagementService _visitationManagementService;
        private readonly ILegalManagementService _legalManagementService;

        public LegalController(IPatientRepository patientRepository, 
                               IDoctorRepository doctorRepository,
                               IVisitationManagementService visitationManagementService,
                               ILegalManagementService legalManagementService)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _visitationManagementService = visitationManagementService;
            _legalManagementService = legalManagementService;
        }

        public ActionResult Index()
        {
            //return RedirectToAction("Reports");
            List<Doctor> doctors = _doctorRepository.GetDoctors();
            List<Patient> patients = _patientRepository.GetPatients();
            return View(new DoctorPatientViewModel { Doctors = doctors, Patients = patients });
            //ViewBag.doctor = new SelectList(doc_pat.doctors, "DoctorId", "FirstName");
            //return View();
        }

        public ActionResult Complaints()
        {
            return View();
        }

        public ActionResult Reports(int doctorId)
        {
            List<Patient> patient = _legalManagementService.DoctorHistory(doctorId);
            return View(patient);
        }
	}
}