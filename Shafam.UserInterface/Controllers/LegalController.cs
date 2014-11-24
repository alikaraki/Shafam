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

        public ActionResult Details(int id, UserRole role, bool showUserCreatedAlert = false)
        {
            if (showUserCreatedAlert)
            {
                ViewBag.ShowUserCreatedAlert = true;
            }

            return View(GetPatient(id));
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
            return RedirectToAction("AssignDoctor", "Legal", new {patientId = patientId});
        }

        // View all visitiation details for a patient's visit
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

        // View all Treatments prescribed to a patient
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

        // View all Tests prescribed to a patient
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


        //Add Test Result for a test prescribed
        [HttpGet]
        public ActionResult AddTestResult(int patientId, int testId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            Test test = _visitationManagementService.GetTestforTestId(testId);
            return View(new TestResultInputModel { Patient = patient, Test = test});
        }

        [HttpPost]
        public ActionResult AddTestResult(int patientId, int testId, TestResultInputModel inputModel)
        {
            _visitationManagementService.UpdateTestsResults(testId, inputModel.Test.Result);
            return RedirectToAction("Tests", "Legal", new { patientId = patientId });
        }

        //Complete Treatment link for a treatment prescribed
        [HttpGet]
        public ActionResult MarkTreatmentAsCompleted(int patientId, int treatmentId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            _visitationManagementService.CompleteTreatment(treatmentId);
            return RedirectToAction("Treatments", "Legal", new { patientId = patientId });
        }

        // Show the schedule of a patient.
        public ActionResult PatientSchedule(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            List<Appointment> appointmentsForPatient = _schedulingService.ViewPatientSchedule(patientId);
            PatientScheduleViewModel patientScheduleViewModel = new PatientScheduleViewModel { Patient = patient, SingleAppointmentViewModels = new List<SingleAppointmentViewModel>() };

            foreach (Appointment appointment in appointmentsForPatient)
            {
                SingleAppointmentViewModel singleAppointment = new SingleAppointmentViewModel();
                singleAppointment.SDoctor = _schedulingService.GetDoctorForAppointment(appointment.AppointmentId);
                singleAppointment.SAppointment = _appointmentRepository.GetAppointment(appointment.AppointmentId);
                patientScheduleViewModel.SingleAppointmentViewModels.Add(singleAppointment);
            }
            return View(patientScheduleViewModel);
        }

        // Show the schedule of a doctor.
        public ActionResult DoctorSchedule(int doctorId)
        {
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            List<Appointment> appointmentsForDoctor = _schedulingService.ViewDoctorSchedule(doctorId);
            DoctorScheduleViewModel doctorScheduleViewModel = new DoctorScheduleViewModel { Doctor = doctor, SingleAppointmentDoctorViewModels = new List<SingleAppointmentDoctorViewModel>() };

            foreach (Appointment appointment in appointmentsForDoctor)
            {
                SingleAppointmentDoctorViewModel singleAppointment = new SingleAppointmentDoctorViewModel();
                singleAppointment.SPatient = _schedulingService.GetPatientForAppointment(appointment.AppointmentId);
                singleAppointment.SAppointment = _appointmentRepository.GetAppointment(appointment.AppointmentId);
                doctorScheduleViewModel.SingleAppointmentDoctorViewModels.Add(singleAppointment);
            }
            return View(doctorScheduleViewModel);    
        }

        // Show the details of a single appointment.
        public ActionResult AppointmentDetails(int appointmentId)
        {
            Appointment appointment = _appointmentRepository.GetAppointment(appointmentId);
            return View(appointment);
        }

        // Cancel an appointment.
        public ActionResult CancelAppointment(int appointmentId)
        {
            Appointment appointment = _appointmentRepository.GetAppointment(appointmentId);
            int doctorId = appointment.DoctorId;
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            _appointmentRepository.CancelAppointment(appointmentId);
            return View(doctor);
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
            return RedirectToAction("DoctorSchedule", "Legal", new { doctorId = doctorId });
        }

        private UserViewModel GetPatient(int id)
        {
            var patient = _patientRepository.GetPatient(id);
            return patient.GetUserViewModel(_accountRepository.GetAccountByUserId(patient.PatientId,UserRole.Patient));
        }

	}
}