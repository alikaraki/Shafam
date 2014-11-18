using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface IPatientManagementService
    {
        Patient ViewPatient(int patientId);

        List<Patient> ViewAllPatients(int doctorId);

        int AddPatient(Patient patient, int doctorId);

        bool AddVisitation(Patient patient, int visitationId);

        bool ReferPatient(int patientId, int referringDocId, int referredDocId);
    }
}