using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.VisitationManagement
{
    public class VisitationManagementService : IVisitationManagementService
    {
        private readonly IVisitationRepository _visitationRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITestRepository _testRepository;

        public VisitationManagementService(IVisitationRepository visitationRepository, 
                                            IMedicationRepository medicationRepository,
                                            ITreatmentRepository treatmentRepository,
                                            ITestRepository testRepository) 
        {
            _visitationRepository = visitationRepository;
            _medicationRepository = medicationRepository;
            _treatmentRepository = treatmentRepository;
            _testRepository = testRepository;
        }

        public Visitation AddVisitation(int doctorId, int patientId, string reason, string notes, 
                                        string medicationName, string medicationQuantity, string medicationInstructions,
                                        string treatmentPrescribed, string testPrescribed) 
        {
            Visitation visitation = _visitationRepository.CreateVisitation(doctorId, patientId, DateTime.Now, reason, notes);
            if (medicationName != null) {Medication medication = _medicationRepository.AddMedication(visitation.VisitationId, medicationName, medicationQuantity, medicationInstructions);}
            if (testPrescribed != null) {Test test = _testRepository.AddTest(visitation.VisitationId, testPrescribed);}
            if (treatmentPrescribed != null) {Treatment treatment = _treatmentRepository.AddTreatment(visitation.VisitationId, treatmentPrescribed);}

            return visitation;
        }

        public Visitation GetVisitationForVisitationId(int visitationId)
        {
            Visitation visitation = _visitationRepository.GetVisitation(visitationId);
            return visitation;
        }
        public IEnumerable<Visitation> GetVisitationsForPatient(int patientId)
        {
            IEnumerable<Visitation> visitationsForPatient= _visitationRepository.GetVisitationsForPatient(patientId);
            return visitationsForPatient;
        }

        public IEnumerable<Medication> GetMedicationsForVisitation(int visitationId) 
        {
            IEnumerable<Medication> medicationsForVisitation = _medicationRepository.GetMedicationsForVisitation(visitationId);
            return medicationsForVisitation;
        }

        public IEnumerable<Treatment> GetTreatmentsforVisitation(int visitationId)
        {
            IEnumerable<Treatment> treatmentsForVisitation = _treatmentRepository.GetTreatmentsForVisitation(visitationId);
            return treatmentsForVisitation;
        }

        public IEnumerable<Test> GetTestsforVisitation(int visitationId)
        {
            IEnumerable<Test> testsForVisitation = _testRepository.GetTestsForVisitation(visitationId);
            return testsForVisitation;
        }

        public void UpdateTestsResults(int testId, string testResult)
        {
            _testRepository.AddTestResult(testId, testResult);
            return;
        }
        public Test GetTestforTestId(int testId)
        {
            Test test = _testRepository.GetTestsForTestId(testId);
            return test;
        }

    }
}
