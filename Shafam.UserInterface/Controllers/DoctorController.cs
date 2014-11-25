using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Shafam.BusinessLogic;
using Shafam.Common.DataModel;
using System.Linq;
using Shafam.DataAccess;
using Shafam.UserInterface.Infrastructure;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Doctor)]
    public class DoctorController : Controller
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientManagementService _patientManagementService;
        private readonly IVisitationManagementService _visitationManagementService;
        private readonly ISchedulingService _schedulingService;
        private readonly IReferralRepository _referralRepository;
        private readonly INotificationManagementService _notificationManagementService;

        public DoctorController(IIdentityProvider identityProvider,
                                IDoctorRepository doctorRepository,
                                IAppointmentRepository appointmentRepository,
                                IPatientRepository patientRepository, 
                                IPatientManagementService patientManagementService,
                                IVisitationManagementService visitationManagementService,
                                ISchedulingService schedulingService,
                                IReferralRepository referralRepository,
                                INotificationManagementService notificationManagementService)
        {
            _identityProvider = identityProvider;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _patientManagementService = patientManagementService;
            _visitationManagementService = visitationManagementService;
            _schedulingService = schedulingService;
            _referralRepository = referralRepository;
            _notificationManagementService = notificationManagementService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            IEnumerable<Notification> notifications =
                _notificationManagementService.GetNotificationsForDoctor(_identityProvider.GetAuthenticatedUserId());

            return View(notifications);
        }

        public ActionResult AcknowledgeNotification(int notificationId, NotificationType type)
        {
            _notificationManagementService.MarkAsSeen(notificationId, type);
            return RedirectToAction("Home");
        }

        //
        // GET: /Doctor/Patients/
        public ActionResult Patients()
        {
            int doctorId = _identityProvider.GetAuthenticatedUserId();
            IEnumerable<Patient> patients = _patientManagementService.ViewAllPatients(doctorId);

            return View(patients);
        }

        //
        // GET: /Doctor/ReferPatient/
        [HttpGet]
        public ActionResult ReferPatient(int patientId)
        {
            int thisDocId = _identityProvider.GetAuthenticatedUserId();
            Patient patient = _patientManagementService.ViewPatient(patientId);
            List<Doctor> allDoctors = _doctorRepository.GetDoctors();
            List<Referral> referralsForDoctor = _referralRepository.GetReferralsForReferringDoctor(thisDocId).ToList();

            var referredDoctors = new List<Doctor>();
            foreach (Referral r in referralsForDoctor)
            {
                if (r.PatientId == patientId)
                    referredDoctors.Add(_doctorRepository.GetDoctor(r.ReferredDoctorId));
            }

            return View(new ReferPatientViewModel { Patient = patient, Doctors = allDoctors, ReferredDoctors = referredDoctors });
        }

        //
        // POST: /Doctor/ReferPatient/
        [HttpPost]
        public ActionResult ReferPatient(ReferPatientViewModel model, int patientId)
        {
            int thisDocId = _identityProvider.GetAuthenticatedUserId();
            // Refer patient to the doctor associated with referredDocId
            _patientManagementService.ReferPatient(patientId, thisDocId, int.Parse(model.ReferredDoctorId));

            return RedirectToAction("ReferPatient", "Doctor", new { patientId = patientId });
        }

        public ActionResult PatientProfile(int patientId)
        {
            Patient patient = _patientManagementService.ViewPatient(patientId);
            return View(patient);
        }

        public ActionResult VisitationDetails(int patientId, int visitationId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            Visitation visitation = _visitationManagementService.GetVisitationForVisitationId(visitationId);
            Doctor doctor = _doctorRepository.GetDoctor(visitation.DoctorId);
            List<Medication> medicationList = _visitationManagementService.GetMedicationsForVisitation(visitationId).ToList<Medication>();
            List<Treatment> treatmentList = _visitationManagementService.GetTreatmentsforVisitation(visitationId).ToList<Treatment>();
            List<Test> testList = _visitationManagementService.GetTestsforVisitation(visitationId).ToList<Test>();

            Medication medication = null;
            Treatment treatment = null;
            Test test = null;

            if (medicationList.Count != 0) medication = medicationList.ElementAt(0);
            if (treatmentList.Count != 0) treatment = treatmentList.ElementAt(0);
            if (testList.Count != 0) test = testList.ElementAt(0);

            VisitationDetailModel visitationDetailModel = new VisitationDetailModel {Patient = patient, 
                                                                                    Visitation = visitation,
                                                                                    Doctor = doctor,
                                                                                    Medication = medication,
                                                                                    Treatment = treatment,
                                                                                    Test = test};

            return View(visitationDetailModel);
        }

        public ActionResult Visitations(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            int doctorId = _identityProvider.GetAuthenticatedUserId();
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatientWithDoctor(patient.PatientId,doctorId);
            return View(new VisitationViewModel { Patient = patient, Doctor = doctor, Visitations = visitationsForPatient});
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

        [HttpGet]
        public ActionResult AddVisitation(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);

            return View(new VisitationInputModel { Patient = patient });
        }

        [HttpPost]
        public ActionResult AddVisitation(int patientId, VisitationInputModel inputModel)
        {
            // Add visitation record to repository
            int doctorId = _identityProvider.GetAuthenticatedUserId();
            Visitation visitation = _visitationManagementService.AddVisitation(doctorId, patientId, inputModel.Reason, inputModel.Notes,
                                                                                inputModel.MedicationName, inputModel.MedicationQuantity,
                                                                                inputModel.MedicationInstructions,
                                                                                inputModel.Treatment, inputModel.Test);

            // Redirect to patient details
            return RedirectToAction("Visitations", "Doctor", new { patientId = patientId });
        }

        // Show the schedule of a doctor.
        public ActionResult Schedule()
        {
            int doctorId = _identityProvider.GetAuthenticatedUserId();
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
    }
}


