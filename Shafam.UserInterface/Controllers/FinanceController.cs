using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.UserInterface.Infrastructure;
using Shafam.DataAccess;
using Shafam.Common.DataModel;
using Shafam.BusinessLogic;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Finance)]
    public class FinanceController : Controller
    {

        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IBillingManagementService _billingManagementService;
        private readonly IPatientManagementService _patientManagementService;

        public FinanceController(IPatientRepository patientRepository,
                                IDoctorRepository doctorRepository,
                                IBillingManagementService billingManagementService,
                                IPatientManagementService patientManagementService)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _billingManagementService = billingManagementService;
            _patientManagementService = patientManagementService;
        }
        
        public ActionResult Index()
        {
            return RedirectToAction("Doctors");
        }

        public ActionResult Doctors()
        {
            IEnumerable<Doctor> doctors = _doctorRepository.GetDoctors();
            return View(doctors);
        }

        public ActionResult Patients()
        {
            List<Patient> patients = _patientRepository.GetPatients();
            return View(patients);
        }

        [HttpGet]
        public ActionResult TimeBill()
        {
            ViewBag.ReturnUrl = Url.Action("TimeBill");
            return View(new TimePeriodInputViewModel { });
        }

        [HttpPost]
        public ActionResult TimeBill(TimePeriodInputViewModel timePeriod)
        {
            DateTime begin = timePeriod.Begin;
            DateTime end = timePeriod.End;
            return RedirectToAction("TimeBillDetails", "Finance", new { begin = begin, end = end});
        }


        public ActionResult DoctorBill(int doctorId)
        {
            List<Treatment> treatments = _billingManagementService.GetTreatmentsForDoctor(doctorId);
            List<Test> tests = _billingManagementService.GetTestsForDoctor(doctorId);
            List<Medication> medications = _billingManagementService.GetMedicationsForDoctor(doctorId);
            Dictionary<string, int> treatmentDict = new Dictionary<string, int>();
            Dictionary<string, int> testDict = new Dictionary<string, int>();
            Dictionary<string, int> medicationDict = new Dictionary<string, int>();

            foreach (Treatment treatment in treatments)
            {
                
                if (treatmentDict.ContainsKey(treatment.TreatmentType))
                {
                    treatmentDict[treatment.TreatmentType]++;
                }
                else
                {
                    treatmentDict.Add(treatment.TreatmentType, 1);
                }
            }

            foreach (Test test in tests)
            {
                if (testDict.ContainsKey(test.Type))
                {
                    testDict[test.Type]++;
                }
                else
                {
                    testDict[test.Type] = 1;
                }
            }

            foreach (Medication medication in medications)
            {
                if (medicationDict.ContainsKey(medication.Name))
                {
                    medicationDict[medication.Name]++;
                }
                else
                {
                    medicationDict[medication.Name] = 1;
                }
            }
            
            DoctorBillViewModel doctorBillViewModel = new DoctorBillViewModel
            {
                Doctor = _doctorRepository.GetDoctor(doctorId),
                NumberOfVisitations = _billingManagementService.GetNumberOfVisitationsForDoctor(doctorId),
                TestDict = testDict,
                TreatmentDict = treatmentDict,
                MedicationDict = medicationDict
            };
            return View(doctorBillViewModel);
        }

        public ActionResult PatientBill(int patientId)
        {
            List<Treatment> treatments = _billingManagementService.GetTreatmentsForPatient(patientId);
            List<Test> tests = _billingManagementService.GetTestsForPatient(patientId);
            List<Medication> medications = _billingManagementService.GetMedicationsForPatient(patientId);
            Dictionary<string, int> treatmentDict = new Dictionary<string, int>();
            Dictionary<string, int> testDict = new Dictionary<string, int>();
            Dictionary<string, int> medicationDict = new Dictionary<string, int>();

            foreach (Treatment treatment in treatments)
            {

                if (treatmentDict.ContainsKey(treatment.TreatmentType))
                {
                    treatmentDict[treatment.TreatmentType]++;
                }
                else
                {
                    treatmentDict.Add(treatment.TreatmentType, 1);
                }
            }

            foreach (Test test in tests)
            {
                if (testDict.ContainsKey(test.Type))
                {
                    testDict[test.Type]++;
                }
                else
                {
                    testDict[test.Type] = 1;
                }
            }

            foreach (Medication medication in medications)
            {
                if (medicationDict.ContainsKey(medication.Name))
                {
                    medicationDict[medication.Name]++;
                }
                else
                {
                    medicationDict[medication.Name] = 1;
                }
            }

            PatientBillViewModel patientBillViewModel = new PatientBillViewModel
            {
                Patient = _patientRepository.GetPatient(patientId),
                NumberOfVisitations = _billingManagementService.GetNumberOfVisitationsForPatient(patientId),
                TestDict = testDict,
                TreatmentDict = treatmentDict,
                MedicationDict = medicationDict
            };
            return View(patientBillViewModel);
        }

        public ActionResult TimeBillDetails(DateTime begin, DateTime end)
        {
            List<Treatment> treatments = _billingManagementService.GetTreatmentsForTime(begin, end);
            List<Test> tests = _billingManagementService.GetTestsForTime(begin, end);
            List<Medication> medications = _billingManagementService.GetMedicationsForTime(begin, end);
            Dictionary<string, int> treatmentDict = new Dictionary<string, int>();
            Dictionary<string, int> testDict = new Dictionary<string, int>();
            Dictionary<string, int> medicationDict = new Dictionary<string, int>();

            foreach (Treatment treatment in treatments)
            {

                if (treatmentDict.ContainsKey(treatment.TreatmentType))
                {
                    treatmentDict[treatment.TreatmentType]++;
                }
                else
                {
                    treatmentDict.Add(treatment.TreatmentType, 1);
                }
            }

            foreach (Test test in tests)
            {
                if (testDict.ContainsKey(test.Type))
                {
                    testDict[test.Type]++;
                }
                else
                {
                    testDict[test.Type] = 1;
                }
            }

            foreach (Medication medication in medications)
            {
                if (medicationDict.ContainsKey(medication.Name))
                {
                    medicationDict[medication.Name]++;
                }
                else
                {
                    medicationDict[medication.Name] = 1;
                }
            }

            TimeBillViewModel timeBillViewModel = new TimeBillViewModel
            {
                NumberOfVisitations = _billingManagementService.GetNumberOfVisitationForTime(begin, end),
                TestDict = testDict,
                TreatmentDict = treatmentDict,
                MedicationDict = medicationDict
            };
            return View(timeBillViewModel);
        }
	}
}