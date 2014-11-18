using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IMedicationRepository
    {
        Medication AddMedication(int visitationId, string name, string quantity, string instructions);

        List<Medication> GetMedicationsForPatient(int patientId);

        List<Medication> GetMedicationsForVisitation(int visitationId);
    }
}