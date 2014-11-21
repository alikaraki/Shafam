using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.UserInterface.Infrastructure;
using Shafam.BusinessLogic;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Staff)]
    public class StaffController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ISchedulingService _schedulingService;

        public StaffController(IPatientRepository patientRepository, ISchedulingService schedulingService,
                                IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _schedulingService = schedulingService;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
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

        public ActionResult PatientSchedule(int patientId)
        {
            List<Appointment> appointments = _schedulingService.ViewPatientSchedule(patientId);
            return View(appointments);
        }

        public ActionResult DoctorSchedule(int doctorId)
        {
            List<Appointment> appointments = _schedulingService.ViewDoctorSchedule(doctorId);
            return View(appointments);
        }

        public ActionResult Doctors()
        {
            IEnumerable<Doctor> doctors = _doctorRepository.GetDoctors();

            return View(doctors);
        }
	}
}