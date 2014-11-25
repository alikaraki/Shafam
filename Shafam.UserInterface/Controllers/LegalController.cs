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
	[Authorize(Roles = UserRoles.Legal)]
	public class LegalController : Controller
	{
		private readonly IPatientRepository _patientRepository;
		private readonly IIdentityProvider _identityProvider;
		private readonly IDoctorRepository _doctorRepository;
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly ISchedulingService _schedulingService;
		private readonly IPatientManagementService _patientManagementService;
		private readonly IAccountManagementService _accountManagementService;
		private readonly IVisitationManagementService _visitationManagementService;

		public LegalController(IPatientRepository patientRepository, 
								IIdentityProvider identityProvider,
								ISchedulingService schedulingService,
								IAppointmentRepository appointmentRepository, 
								IDoctorRepository doctorRepository,
								IPatientManagementService patientManagementService,
								IVisitationManagementService visitationManagementService,
								IAccountRepository accountRepository,
								IAccountManagementService accountManagementService)
		{
			_patientRepository = patientRepository;
			_identityProvider = identityProvider;
			_schedulingService = schedulingService;
			_appointmentRepository = appointmentRepository;
			_doctorRepository = doctorRepository;
			_patientManagementService = patientManagementService;
			_accountManagementService = accountManagementService;
			_accountRepository = accountRepository;
			_visitationManagementService = visitationManagementService;
		}

		public ActionResult Index()
		{
			return RedirectToAction("Patients");
		}

		public ActionResult Patients()
		{
			IEnumerable<Patient> patients = _patientManagementService.AllPatients();

			return View(patients);
		}

        // Show the visitations of a patient
        public ActionResult Visitations(int patientId)
		{
			Patient patient = _patientRepository.GetPatient(patientId);
			IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatient(patient.PatientId);

            Dictionary<int,Doctor> doctors = new Dictionary<int,Doctor>();
            foreach (Visitation visitation in visitationsForPatient)
            {
                doctors.Add(visitation.DoctorId,_doctorRepository.GetDoctor(visitation.DoctorId));
            }

			return View(new VisitationViewModel { Patient = patient, Visitations = visitationsForPatient, Doctors = doctors });
		}

		// Show the visitations of a doctor
		public ActionResult DoctorVisitations(int doctorId)
		{
			Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            IEnumerable<Visitation> visitationsForDoctor = _visitationManagementService.GetVisitationsForDoctor(doctor.DoctorId);
            return View(new VisitationViewModel { Doctor = doctor, Visitations = visitationsForDoctor });   
		}

		// Show a list of all doctors.
		public ActionResult Doctors()
		{
			IEnumerable<Doctor> doctors = _doctorRepository.GetDoctors();
			return View(doctors);
		}
		
		// Show the patients of a single doctor.
		public ActionResult DoctorPatients(int doctorId)
		{
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            List<Patient> patients = _patientManagementService.ViewAllPatients(doctorId);
            return View(new PatientViewModel { Doctor = doctor, Patients = patients });
		}

		private UserViewModel GetPatient(int id)
		{
			var patient = _patientRepository.GetPatient(id);
			return patient.GetUserViewModel(_accountRepository.GetAccountByUserId(patient.PatientId,UserRole.Patient));
		}

	}
}