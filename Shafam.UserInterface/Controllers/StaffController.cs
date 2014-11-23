using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.UserInterface.Infrastructure;
using Shafam.BusinessLogic;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Staff)]
    public class StaffController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISchedulingService _schedulingService;
        private readonly IPatientManagementService _patientManagementService;
        private readonly IAccountManagementService _accountManagementService;

        public StaffController(IPatientRepository patientRepository, ISchedulingService schedulingService,
                                IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository,
                                 IPatientManagementService patientManagementService, IAccountManagementService accountManagementService,
                                    IAccountRepository accountRepository)
        {
            _patientRepository = patientRepository;
            _schedulingService = schedulingService;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientManagementService = patientManagementService;
            _accountManagementService = accountManagementService;
            _accountRepository = accountRepository;
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

        // 
        // GET: /Staff/AddPatient
        [HttpGet]
        public ActionResult AddPatient()
        {
            ViewBag.ReturnUrl = Url.Action("AddPatient");
            return View(new PatientInputModel());
        }

        // 
        // POST: /Staff/AddPatient
        [HttpPost]
        public ActionResult AddPatient(PatientInputModel model)
        {
            // Add Patient to patient repository
            Patient patient = _patientManagementService.AddPatient(model.FirstName, model.LastName, model.Age, 
                                                                    model.Gender, model.HealthCardNumber, model.PhoneNumber, model.Address);
            // Create patient account
            _accountManagementService.CreateAccountForPatient(patient.PatientId);

            // Redirect to All Patients
            return RedirectToAction("Details", "Staff", new { id = patient.PatientId, role = UserRole.Patient, showUserCreatedAlert = true });
        }

        public ActionResult Details(int id, UserRole role, bool showUserCreatedAlert = false)
        {
            if (showUserCreatedAlert)
            {
                ViewBag.ShowUserCreatedAlert = true;
            }

            return View(GetPatient(id));
        }

        public ActionResult PatientProfile(int patientId)
        {
            Patient patient = _patientManagementService.ViewPatient(patientId);
            return View(patient);
        }

        //
        // GET: /Staff/AssignDoctor
        [HttpGet]
        public ActionResult AssignDoctor(int patientId)
        {
            Patient patient = _patientManagementService.ViewPatient(patientId);
            List<Doctor> allDoctors = _doctorRepository.GetDoctors();
            List<Doctor> assignedDoctors = _patientRepository.GetDoctorsForPatient(patientId);
            ViewBag.ReturnUrl = Url.Action("AssignDoctor");
            return View(new DoctorAssignmentViewModel { Patient = patient, Doctors = allDoctors, AssignedDoctors = assignedDoctors });
        }

        //
        // POST: /Staff/AssignDoctor
        [HttpPost]
        public ActionResult AssignDoctor(DoctorAssignmentViewModel model)
        {
            // Assign patient to a specific doctor
            _patientManagementService.AssignDoctorToPatient(int.Parse(model.AssignedDoctorId), model.Patient.PatientId);

            // Redirect to doctor assignment page
            return RedirectToAction("AssignDoctor", "Staff", model.Patient.PatientId);
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

        public ActionResult DoctorProfile(int doctorId)
        {
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            return View(doctor);
        }

        public ActionResult NewAppointment(int doctorId)
        {
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            return View(doctor);
        }

        private UserViewModel GetPatient(int id)
        {
            var patient = _patientRepository.GetPatient(id);
            return patient.GetUserViewModel(_accountRepository.GetAccountByUserId(patient.PatientId));
        }

        // --- FOR TESTING PURPOSES ---
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
	}
}