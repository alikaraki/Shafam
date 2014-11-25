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
			List<Doctor> doctors = _patientRepository.GetDoctorsForPatient(patientId);
			IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatient(patient.PatientId);
			
			VisitationViewModel visitationViewModel = new VisitationViewModel { Patient = patient,
																				Visitations = visitationsForPatient,
																				DoctorList = doctors,
																				Medications = new List<SingleMedicationModel>() };

			foreach (Visitation visitation in visitationsForPatient)
			{
				IEnumerable<Medication> medicationsForVisitation = _visitationManagementService.GetMedicationsForVisitation(visitation.VisitationId);
				foreach (Medication medication in medicationsForVisitation)
				{
					SingleMedicationModel singleMedication = new SingleMedicationModel
					{
						DateTime = visitation.DateTime,
						Reason = visitation.Reason,
						Medication = medication,
						VisitationId = visitation.VisitationId
					};
					visitationViewModel.Medications.Add(singleMedication);
				}
			}

			return View(visitationViewModel);
		}

		// Show the visitations of a doctor
		public ActionResult DoctorVisitations(int doctorId)
		{
			Doctor doctor = _doctorRepository.GetDoctor(doctorId);
			IEnumerable<Visitation> visitationsForDoctor = _visitationManagementService.GetVisitationsForDoctor(doctor.DoctorId);
			VisitationViewModel visitationViewModel = new VisitationViewModel {	Visitations = visitationsForDoctor,
																				Doctor = doctor,
																				Medications = new List<SingleMedicationModel>() };
			foreach (Visitation visitation in visitationsForDoctor)
			{
				IEnumerable<Medication> medicationsForVisitation = _visitationManagementService.GetMedicationsForVisitation(visitation.VisitationId);
				foreach (Medication medication in medicationsForVisitation)
				{
					SingleMedicationModel singleMedication = new SingleMedicationModel
					{
						DateTime = visitation.DateTime,
						Reason = visitation.Reason,
						Medication = medication,
						VisitationId = visitation.VisitationId
					};
					visitationViewModel.Medications.Add(singleMedication);
				}
			}
			return View(visitationViewModel);   
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

		// Show patient's visitations of a specific doctor
		public ActionResult PatientVisitations(int doctorId, int patientId)
		{
			Doctor doctor = _doctorRepository.GetDoctor(doctorId);
			Patient patient = _patientRepository.GetPatient(patientId);
			IEnumerable<Visitation> visitations = _visitationManagementService.GetVisitationsForPatient(patientId).Where(v => v.DoctorId == doctorId);
			VisitationViewModel visitationViewModel = new VisitationViewModel { Patient = patient,
																				Visitations = visitations,
																				Doctor = doctor,
																				Medications = new List<SingleMedicationModel>() };
            foreach (Visitation visitation in visitations)
			{
				IEnumerable<Medication> medicationsForVisitation = _visitationManagementService.GetMedicationsForVisitation(visitation.VisitationId);
				foreach (Medication medication in medicationsForVisitation)
				{
					SingleMedicationModel singleMedication = new SingleMedicationModel
					{
						DateTime = visitation.DateTime,
						Reason = visitation.Reason,
						Medication = medication,
						VisitationId = visitation.VisitationId
					};
					visitationViewModel.Medications.Add(singleMedication);
				}
			}
			return View(visitationViewModel);
		}

		private UserViewModel GetPatient(int id)
		{
			var patient = _patientRepository.GetPatient(id);
			return patient.GetUserViewModel(_accountRepository.GetAccountByUserId(patient.PatientId,UserRole.Patient));
		}
	}
}