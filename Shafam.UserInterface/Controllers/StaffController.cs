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
        private readonly IVisitationManagementService _visitationManagementService;

        public StaffController(IPatientRepository patientRepository, 
                                ISchedulingService schedulingService,
                                IAppointmentRepository appointmentRepository, 
                                IDoctorRepository doctorRepository,
                                IPatientManagementService patientManagementService,
                                IVisitationManagementService visitationManagementService)
        {
            _patientRepository = patientRepository;
            _schedulingService = schedulingService;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientManagementService = patientManagementService;
            _visitationManagementService = visitationManagementService;
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
        // GET: /Staff/AssignDoctor
        [HttpGet]
        public ActionResult AssignDoctor(int patientId)
        {
            Patient patient = _patientManagementService.ViewPatient(patientId);
            List<Doctor> doctors = _patientRepository.GetDoctorsForPatient(patientId);
            ViewBag.ReturnUrl = Url.Action("AssignDoctor");
            return View(new DoctorAssignmentViewModel { Patient = patient, Doctors = doctors });
        }

        //
        // POST: /Staff/AssignDoctor
        [HttpPost]
        public ActionResult AssignDoctor(DoctorAssignmentViewModel model)
        {
            // Assign patient to a specific doctor
            _patientManagementService.AssignDoctorToPatient(int.Parse(model.AssignedDoctorId), model.Patient.PatientId);

            // Redirect to doctor assignment page
            return RedirectToAction("AssignDoctor", "Staff");
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
                        Medication = medication
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
                        Treatment = treatment
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
                        Test = test
                    };
                    testViewModel.Tests.Add(singleTest);
                }
            }
            return View(testViewModel);
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