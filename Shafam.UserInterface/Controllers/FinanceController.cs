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
            return RedirectToAction("TimeBillDetails", "Finance", new { begin = begin, end = end });
        }


        public ActionResult DoctorBill(int doctorId)
        {
            List<Treatment> treatments = _billingManagementService.GetTreatmentsForDoctor(doctorId);
            List<Test> tests = _billingManagementService.GetTestsForDoctor(doctorId);
            List<Medication> medications = _billingManagementService.GetMedicationsForDoctor(doctorId);
            Dictionary<string, Tuple<double, int, double>> treatmentDict = new Dictionary<string, Tuple<double, int, double>>();
            Dictionary<string, Tuple<double, int, double>> testDict = new Dictionary<string, Tuple<double, int, double>>();
            Dictionary<string, Tuple<double, int, double>> medicationDict = new Dictionary<string, Tuple<double, int, double>>();
            int count = 0;

            foreach (Treatment treatment in treatments)
            {
                if (!String.IsNullOrEmpty(treatment.TreatmentType))
                {
                    if (treatmentDict.ContainsKey(treatment.TreatmentType))
                    {
                        count = treatmentDict[treatment.TreatmentType].Item2 + 1;
                        Tuple<double, int, double> treatmentTuple = new Tuple<double, int, double>(treatment.Rate, count, treatment.Rate * count);
                        treatmentDict[treatment.TreatmentType] = treatmentTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> treatmentTuple = new Tuple<double, int, double>(treatment.Rate, 1, treatment.Rate);
                        treatmentDict.Add(treatment.TreatmentType, treatmentTuple);
                    }
                }
            }

            count = 0;
            foreach (Test test in tests)
            {
                if (!String.IsNullOrEmpty(test.Type))
                {
                    if (testDict.ContainsKey(test.Type))
                    {
                        count = testDict[test.Type].Item2 + 1;
                        Tuple<double, int, double> testTuple = new Tuple<double, int, double>(test.Rate, count, test.Rate * count);
                        testDict[test.Type] = testTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> testTuple = new Tuple<double, int, double>(test.Rate, 1, test.Rate);
                        testDict.Add(test.Type, testTuple);
                    }
                }
            }

            count = 0;
            foreach (Medication medication in medications)
            {
                if (!String.IsNullOrEmpty(medication.Name))
                {
                    if (medicationDict.ContainsKey(medication.Name))
                    {
                        count = medicationDict[medication.Name].Item2 + 1;
                        Tuple<double, int, double> medicationTuple = new Tuple<double, int, double>(medication.Rate, count, medication.Rate * count);
                        medicationDict[medication.Name] = medicationTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> medicationTuple = new Tuple<double, int, double>(medication.Rate, 1, medication.Rate);
                        medicationDict.Add(medication.Name, medicationTuple);
                    }
                }
            }


            // CALCULATE BILL AMOUNT
            int numVisits = _billingManagementService.GetNumberOfVisitationsForDoctor(doctorId);
            double Amount = (numVisits * 100)
                            + testDict.Sum(ix => ix.Value.Item3)
                            + treatmentDict.Sum(ix => ix.Value.Item3)
                            + medicationDict.Sum(ix => ix.Value.Item3);

            DoctorBillViewModel doctorBillViewModel = new DoctorBillViewModel
            {
                Doctor = _doctorRepository.GetDoctor(doctorId),
                NumberOfVisitations = numVisits,
                VisitationCost = 100 * numVisits,
                TestDict = testDict,
                TreatmentDict = treatmentDict,
                MedicationDict = medicationDict,
                BillAmount = Amount
            };
            return View(doctorBillViewModel);
        }

        public ActionResult PatientBill(int patientId)
        {
            List<Treatment> treatments = _billingManagementService.GetTreatmentsForPatient(patientId);
            List<Test> tests = _billingManagementService.GetTestsForPatient(patientId);
            List<Medication> medications = _billingManagementService.GetMedicationsForPatient(patientId);
            Dictionary<string, Tuple<double, int, double>> treatmentDict = new Dictionary<string, Tuple<double, int, double>>();
            Dictionary<string, Tuple<double, int, double>> testDict = new Dictionary<string, Tuple<double, int, double>>();
            Dictionary<string, Tuple<double, int, double>> medicationDict = new Dictionary<string, Tuple<double, int, double>>();
            int count = 0;

            foreach (Treatment treatment in treatments)
            {
                if (!String.IsNullOrEmpty(treatment.TreatmentType))
                {
                    if (treatmentDict.ContainsKey(treatment.TreatmentType))
                    {
                        count = treatmentDict[treatment.TreatmentType].Item2 + 1;
                        Tuple<double, int, double> treatmentTuple = new Tuple<double, int, double>(treatment.Rate, count, treatment.Rate * count);
                        treatmentDict[treatment.TreatmentType] = treatmentTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> treatmentTuple = new Tuple<double, int, double>(treatment.Rate, 1, treatment.Rate);
                        treatmentDict.Add(treatment.TreatmentType, treatmentTuple);
                    }
                }
            }

            count = 0;
            foreach (Test test in tests)
            {
                if (!String.IsNullOrEmpty(test.Type))
                {
                    if (testDict.ContainsKey(test.Type))
                    {
                        count = testDict[test.Type].Item2 + 1;
                        Tuple<double, int, double> testTuple = new Tuple<double, int, double>(test.Rate, count, test.Rate * count);
                        testDict[test.Type] = testTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> testTuple = new Tuple<double, int, double>(test.Rate, 1, test.Rate);
                        testDict.Add(test.Type, testTuple);
                    }
                }
            }

            count = 0;
            foreach (Medication medication in medications)
            {
                if (!String.IsNullOrEmpty(medication.Name))
                {
                    if (medicationDict.ContainsKey(medication.Name))
                    {
                        count = medicationDict[medication.Name].Item2 + 1;
                        Tuple<double, int, double> medicationTuple = new Tuple<double, int, double>(medication.Rate, count, medication.Rate * count);
                        medicationDict[medication.Name] = medicationTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> medicationTuple = new Tuple<double, int, double>(medication.Rate, 1, medication.Rate);
                        medicationDict.Add(medication.Name, medicationTuple);
                    }
                }
            }


            // CALCULATE BILL AMOUNT
            int numVisits = _billingManagementService.GetNumberOfVisitationsForPatient(patientId);
            double Amount = (numVisits * 100)
                            + testDict.Sum(ix => ix.Value.Item3)
                            + treatmentDict.Sum(ix => ix.Value.Item3)
                            + medicationDict.Sum(ix => ix.Value.Item3);

            PatientBillViewModel patientBillViewModel = new PatientBillViewModel
            {
                Patient = _patientRepository.GetPatient(patientId),
                NumberOfVisitations = numVisits,
                VisitationCost = 100 * numVisits,
                TestDict = testDict,
                TreatmentDict = treatmentDict,
                MedicationDict = medicationDict,
                BillAmount = Amount
            };
            return View(patientBillViewModel);
        }

        public ActionResult TimeBillDetails(DateTime begin, DateTime end)
        {
            List<Treatment> treatments = _billingManagementService.GetTreatmentsForTime(begin, end);
            List<Test> tests = _billingManagementService.GetTestsForTime(begin, end);
            List<Medication> medications = _billingManagementService.GetMedicationsForTime(begin, end);
            Dictionary<string, Tuple<double, int, double>> treatmentDict = new Dictionary<string, Tuple<double, int, double>>();
            Dictionary<string, Tuple<double, int, double>> testDict = new Dictionary<string, Tuple<double, int, double>>();
            Dictionary<string, Tuple<double, int, double>> medicationDict = new Dictionary<string, Tuple<double, int, double>>();
            int count = 0;

            foreach (Treatment treatment in treatments)
            {
                if (!String.IsNullOrEmpty(treatment.TreatmentType))
                {
                    if (treatmentDict.ContainsKey(treatment.TreatmentType))
                    {
                        count = treatmentDict[treatment.TreatmentType].Item2 + 1;
                        Tuple<double, int, double> treatmentTuple = new Tuple<double, int, double>(treatment.Rate, count, treatment.Rate * count);
                        treatmentDict[treatment.TreatmentType] = treatmentTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> treatmentTuple = new Tuple<double, int, double>(treatment.Rate, 1, treatment.Rate);
                        treatmentDict.Add(treatment.TreatmentType, treatmentTuple);
                    }
                }
            }

            count = 0;
            foreach (Test test in tests)
            {
                if (!String.IsNullOrEmpty(test.Type))
                {
                    if (testDict.ContainsKey(test.Type))
                    {
                        count = testDict[test.Type].Item2 + 1;
                        Tuple<double, int, double> testTuple = new Tuple<double, int, double>(test.Rate, count, test.Rate * count);
                        testDict[test.Type] = testTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> testTuple = new Tuple<double, int, double>(test.Rate, 1, test.Rate);
                        testDict.Add(test.Type, testTuple);
                    }
                }
            }

            count = 0;
            foreach (Medication medication in medications)
            {
                if (!String.IsNullOrEmpty(medication.Name))
                {
                    if (medicationDict.ContainsKey(medication.Name))
                    {
                        count = medicationDict[medication.Name].Item2 + 1;
                        Tuple<double, int, double> medicationTuple = new Tuple<double, int, double>(medication.Rate, count, medication.Rate * count);
                        medicationDict[medication.Name] = medicationTuple;
                    }
                    else
                    {
                        Tuple<double, int, double> medicationTuple = new Tuple<double, int, double>(medication.Rate, 1, medication.Rate);
                        medicationDict.Add(medication.Name, medicationTuple);
                    }
                }
            }


            // CALCULATE BILL AMOUNT
            int numVisits = _billingManagementService.GetNumberOfVisitationForTime(begin, end);
            double Amount = (numVisits * 100)
                            + testDict.Sum(ix => ix.Value.Item3)
                            + treatmentDict.Sum(ix => ix.Value.Item3)
                            + medicationDict.Sum(ix => ix.Value.Item3);

            TimeBillViewModel timeBillViewModel = new TimeBillViewModel
            {
                NumberOfVisitations = numVisits,
                VisitationCost = 100 * numVisits,
                TestDict = testDict,
                TreatmentDict = treatmentDict,
                MedicationDict = medicationDict,
                BillAmount = Amount
            };
            return View(timeBillViewModel);
        }
    }
}