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

        IEnumerable<Visitation> GetVisitationsForPatient(int patientId);

        IEnumerable<Medication> GetMedicationsForVisitation(int visitationId);

        IEnumerable<Treatment> GetTreatmentsforVisitation(int visitationId);

        IEnumerable<Test> GetTestsforVisitation(int visitationId);
    }
}
