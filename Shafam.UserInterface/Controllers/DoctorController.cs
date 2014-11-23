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
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientManagementService _patientManagementService;
        private readonly IVisitationManagementService _visitationManagementService;
        private readonly ISchedulingService _schedulingService;
        

        public DoctorController(IIdentityProvider identityProvider,
                                IDoctorRepository doctorRepository,
                                IPatientRepository patientRepository, 
                                IPatientManagementService patientManagementService,
                                IVisitationManagementService visitationManagementService,
                                ISchedulingService schedulingService)
        {
            _identityProvider = identityProvider;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _patientManagementService = patientManagementService;
            _visitationManagementService = visitationManagementService;
            _schedulingService = schedulingService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            return View();
        }

        //
        // GET: /Patient/
        public ActionResult Patients()
        {
            //IEnumerable<Patient> patients = _patientRepository.GetPatients();
            int doctorId = _identityProvider.GetAuthenticatedUserId();
            IEnumerable<Patient> patients = _patientManagementService.ViewAllPatients(doctorId);

            return View(patients);
        }

        // GETS RANDOM PATIENTS FROM DATABASE
        // *NOTE*: THIS FUNCTION IS FOR TESTING PURPOSES 
        // GET: /Patient/
        public ActionResult RandomPatients()
        {
            IEnumerable<Patient> patients = _patientRepository.GetPatients();
            //IEnumerable<Patient> patients = _patientManagementService.ViewAllPatients(doctorId);

            return View(patients);
        }

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

            return RedirectToAction("RandomPatients");
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
                                                                                    Medication = medication,
                                                                                    Treatment = treatment,
                                                                                    Test = test};

            return View(visitationDetailModel);
        }

        public ActionResult Visitations(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            IEnumerable<Visitation> visitationsForPatient = _visitationManagementService.GetVisitationsForPatient(patient.PatientId);
            return View(new VisitationViewModel { Patient = patient, Visitations = visitationsForPatient});
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
            DoctorScheduleViewModel doctorAppointment = new DoctorScheduleViewModel();
            doctorAppointment.Doctor = _doctorRepository.GetDoctor(doctorId);
            doctorAppointment.Appointments = _schedulingService.ViewDoctorSchedule(doctorId);
            return View(doctorAppointment);
        }
    }
}


