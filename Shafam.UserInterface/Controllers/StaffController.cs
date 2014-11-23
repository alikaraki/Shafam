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
        private readonly ISchedulingService _schedulingService;
        private readonly IPatientManagementService _patientManagementService;

        public StaffController(IPatientRepository patientRepository, ISchedulingService schedulingService,
                                IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository,
                                 IPatientManagementService patientManagementService)
        {
            _patientRepository = patientRepository;
            _schedulingService = schedulingService;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientManagementService = patientManagementService;
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

            // Redirect to All Patients
            return RedirectToAction("Patients", "Staff");
        }

        public ActionResult PatientProfile(int patientId)
        {
            //Patient patient = _patientRepository.GetPatient(patientId);
            Patient patient = _patientManagementService.ViewPatient(patientId);
            return View(patient);
        }

        //
        // GET: /Staff/AssignDoctorToPatient
        [HttpGet]
        public ActionResult AssignDoctor(int patientId)
        {
            Patient patient = _patientManagementService.ViewPatient(patientId);
            List<Doctor> doctors = _patientRepository.GetDoctorsForPatient(patientId);
            ViewBag.ReturnUrl = Url.Action("AssignDoctor");
            return View(new DoctorAssignmentViewModel { Patient = patient, Doctors = doctors });
        }

        //
        // POST: /Staff/AssignDoctorToPatient
        [HttpPost]
        public ActionResult AssignDoctor(DoctorAssignmentViewModel model)
        {

            // Assign patient to a specific doctor
            _patientManagementService.AssignDoctorToPatient(model.AssignedDoctor.DoctorId, model.Patient.PatientId);

            // Redirect to doctor assignment page
            return RedirectToAction("AssignDoctor", "Staff");
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

        // Show the schedule of a patient.
        public ActionResult PatientSchedule(int patientId)
        {
            List<Appointment> appointments = _schedulingService.ViewPatientSchedule(patientId);
            return View(appointments);
        }

        // Show the schedule of a doctor.
        public ActionResult DoctorSchedule(int doctorId)
        {
            DoctorScheduleViewModel doctorAppointment = new DoctorScheduleViewModel();
            doctorAppointment.Doctor = _doctorRepository.GetDoctor(doctorId);
            doctorAppointment.Appointments = _appointmentRepository.GetAppointmentsForDoctor(doctorId);
            return View(doctorAppointment);
        }

        // Show a list of all doctors.
        public ActionResult Doctors()
        {
            IEnumerable<Doctor> doctors = _doctorRepository.GetDoctors();
            return View(doctors);
        }
        
        // Show the details of a single doctor.
        public ActionResult DoctorProfile(int doctorId)
        {
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            return View(doctor);
        }

        // Add a new appointment for a doctor.
        [HttpGet]
        public ActionResult NewAppointment(int doctorId)
        {
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            ViewBag.ReturnUrl = Url.Action("NewAppointment");
            return View(new AppointmentInputViewModel {Doctor = doctor});
        }

        [HttpPost]
        public ActionResult NewAppointment(AppointmentInputViewModel newAppointment, int doctorId)
        {
            _schedulingService.AddAppointment(newAppointment.PatientID, doctorId,
                                                newAppointment.DateTime, newAppointment.Reason); 
            // Redirect to Doctor Schedule
            return RedirectToAction("DoctorSchedule", "Staff", new { doctorId = doctorId });
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