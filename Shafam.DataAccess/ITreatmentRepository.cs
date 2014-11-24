using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface ITreatmentRepository
    {
        Treatment AddTreatment(int visitationId, string treatmentType);

        List<Treatment> GetTreatmentsForPatient(int patientId);

        List<Treatment> GetTreatmentsForVisitation(int visitationId);

        void MarkAsCompleted(int treatmentId);
    }
}
