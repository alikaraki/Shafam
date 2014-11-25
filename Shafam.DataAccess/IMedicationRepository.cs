using System.Collections.Generic;
using Shafam.Common.DataModel;
using System;

namespace Shafam.DataAccess
{
    public interface IMedicationRepository
    {
        Medication AddMedication(int visitationId, string name, string quantity, string instructions);

        List<Medication> GetMedicationsForPatient(int patientId);

        List<Medication> GetMedicationsForVisitation(int visitationId);

        List<Medication> GetMedicationsForDoctor(int doctorId);

        List<Medication> GetMedicationsForTime(DateTime begin, DateTime end);
    }
}