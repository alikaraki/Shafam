using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;


namespace Shafam.BusinessLogic
{
    public interface IVisitationManagementService
    {
        Visitation AddVisitation(int doctorId, int patientId, string reason, string notes,
                                        string medicationName, string medicationQuantity, string medicationInstructions,
                                        string treatmentPrescribed, string testPrescribed);

        Visitation GetVisitationForVisitationId(int visitationId);

        IEnumerable<Visitation> GetVisitationsForPatient(int patientId);

        IEnumerable<Visitation> GetVisitationsForDoctor(int doctorId);

        IEnumerable<Visitation> GetVisitationsForPatientWithDoctor(int patientId, int doctorId);

        IEnumerable<Medication> GetMedicationsForVisitation(int visitationId);

        IEnumerable<Treatment> GetTreatmentsforVisitation(int visitationId);

        IEnumerable<Test> GetTestsforVisitation(int visitationId);

        void UpdateTestsResults(int testId, string testResult);

        Test GetTestforTestId(int testId);

        void CompleteTreatment(int treatmentId);
    }
}
