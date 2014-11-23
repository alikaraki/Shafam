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
        private readonly IIdentityProvider _identityProvider;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISchedulingService _schedulingService;
        private readonly IPatientManagementService _patientManagementService;
        private readonly IAccountManagementService _accountManagementService;
        private readonly IVisitationManagementService _visitationManagementService;

        public StaffController(IPatientRepository patientRepository, 
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
            IEnumerable<Patient> patients = _patientManagementService.ViewPatientsForStaff(_identityProvider.GetAuthenticatedUserId());

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

            return View(new DoctorAssignmentViewModel { Patient = patient, Doctors = allDoctors, AssignedDoctors = assignedDoctors });
        }

        //
        // POST: /Staff/AssignDoctor
        [HttpPost]
        public ActionResult AssignDoctor(DoctorAssignmentViewModel model, int patientId)
        {
            // Assign patient to a specific doctor
            _patientManagementService.AssignDoctorToPatient(int.Parse(model.AssignedDoctorId), patientId);

            // Redirect to doctor assignment page
            return RedirectToAction("PatientProfile", "Staff", new {patientId = patientId});
        }

        public ActionResult VisitationDetails(int patientId, int visitationId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            Visitation visitation = _visitationManagementService.GetVisitationForVisitationId(visitationId);
            List<Medication> medicationList = _visitationManagementService.GetMedicationsForVisitation(visitationId).ToList<Medication>();
            List<Treatment> treatmentList = _visitationManagementService.GetTreatmentsforVisitation(visitationId).ToList<Treatment>();
            List<Test> testList = _visitationManagementService.GetTestsforVisitation(visitationId).ToList<Test>();

            Medication medication = null;
            Treatment treatment = null;
            Test test = null;

            if (medicationList.Count != 0) medication = medicationList.ElementAt(0);
            if (treatmentList.Count != 0) treatment = treatmentList.ElementAt(0);
            if (testList.Count != 0) test = testList.ElementAt(0);

            VisitationDetailModel visitationDetailModel = new VisitationDetailModel
            {
                Patient = patient,
                Visitation = visitation,
                Medication = medication,
                Treatment = treatment,
                Test = test
            };

            return View(visitationDetailModel);
        }

        public ActionResult Visitations(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatient(patient.PatientId);
            return View(new VisitationViewModel { Patient = patient, Visitations = visitationsForPatient });
        }


        public ActionResult Medication(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatient(patientId);
            MedicationViewModel medicationViewModel = new MedicationViewModel { Patient = patient, Medications = new List<SingleMedicationModel>() };

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
                    medicationViewModel.Medications.Add(singleMedication);
                }
            }
            return View(medicationViewModel);
        }

        public ActionResult Treatments(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatient(patientId);
            TreatmentViewModel treatmentViewModel = new TreatmentViewModel { Patient = patient, Treatments = new List<SingleTreatmentModel>() };

            foreach (Visitation visitation in visitationsForPatient)
            {
                IEnumerable<Treatment> treatmentsForVisitation = _visitationManagementService.GetTreatmentsforVisitation(visitation.VisitationId);
                foreach (Treatment treatment in treatmentsForVisitation)
                {
                    SingleTreatmentModel singleTreatment = new SingleTreatmentModel
                    {
                        DateTime = visitation.DateTime,
                        Reason = visitation.Reason,
                        Treatment = treatment,
                        VisitationId = visitation.VisitationId
                    };
                    treatmentViewModel.Treatments.Add(singleTreatment);
                }
            }
            return View(treatmentViewModel);
        }

        public ActionResult Tests(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatient(patientId);
            TestViewModel testViewModel = new TestViewModel { Patient = patient, Tests = new List<SingleTestModel>() };

            foreach (Visitation visitation in visitationsForPatient)
            {
                IEnumerable<Test> testsForVisitation = _visitationManagementService.GetTestsforVisitation(visitation.VisitationId);
                foreach (Test test in testsForVisitation)
                {
                    SingleTestModel singleTest = new SingleTestModel
                    {
                        DateTime = visitation.DateTime,
                        Reason = visitation.Reason,
                        Test = test,
                        VisitationId = visitation.VisitationId
                    };
                    testViewModel.Tests.Add(singleTest);
                }
            }
            return View(testViewModel);
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

        private UserViewModel GetPatient(int id)
        {
            var patient = _patientRepository.GetPatient(id);
            return patient.GetUserViewModel(_accountRepository.GetAccountByUserId(patient.PatientId,UserRole.Patient));
        }

	}
}