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
    [Authorize(Roles = UserRoles.Patient)]
    public class PatientController : Controller
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IPatientRepository _patientRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientManagementService _patientManagementService;
        private readonly IVisitationManagementService _visitationManagementService;
        private readonly ISchedulingService _schedulingService;

        public PatientController(IIdentityProvider identityProvider, 
                                IPatientRepository patientRepository,
                                IAppointmentRepository appointmentRepository,
                                IPatientManagementService patientManagementService, 
                                IVisitationManagementService visitationManagementService,
                                ISchedulingService schedulingService)
        {
            _identityProvider = identityProvider;
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
            _patientManagementService = patientManagementService;
            _visitationManagementService = visitationManagementService;
            _schedulingService = schedulingService;
        }
        public ActionResult Index()
        {
            return RedirectToAction("Appointments");
        }

        public ActionResult Appointments()
        {
            int patientId = _identityProvider.GetAuthenticatedUserId();
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

        // Add a new appointment for a doctor.
        [HttpGet]
        public ActionResult RequestAppointment(int patientId)
        {
            Patient patient = _patientManagementService.ViewPatient(patientId);
            ViewBag.ReturnUrl = Url.Action("RequestAppointment");
            return View(new AppointmentRequestViewModel { Patient = patient });
        }

        [HttpPost]
        public ActionResult RequestAppointment(AppointmentRequestViewModel appointmentRequest, int patientId)
        {
            _schedulingService.RequestAppointment(patientId, null, appointmentRequest.Reason);
            // Redirect to Appointments
            return RedirectToAction("RequestMessage", "Patient");
        }

        public ActionResult RequestMessage()
        {
            return View();
        }
        
        public ActionResult Medication()
        {
            int patientId = _identityProvider.GetAuthenticatedUserId();
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
                        Medication = medication
                    };
                    medicationViewModel.Medications.Add(singleMedication);
                }
            }
            return View(medicationViewModel);
        }

        public ActionResult Tests()
        {
            int patientId = _identityProvider.GetAuthenticatedUserId();
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
                        Test = test
                    };
                    testViewModel.Tests.Add(singleTest);
                }
            }
            return View(testViewModel);
        }
	}
}