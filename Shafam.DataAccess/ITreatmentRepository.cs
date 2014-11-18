using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface ITreatmentRepository
    {
        Treatment AddTreatment(int visitationId, string treatmentType);

        IEnumerable<Treatment> GetTreatmentsForPatient(int patientId);

        IEnumerable<Treatment> GetTreatmentsForVisitation(int patientId);
    }
}
