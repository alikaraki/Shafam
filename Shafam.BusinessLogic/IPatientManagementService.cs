using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    interface IPatientManagementService
    {
        Patient ViewPatient(int patientId);
        List<Patient> ViewAllPatients(int doctorId);
        int AddPatient(Patient patient, int doctorId);
        bool UpdatePatient(Patient patient, int visitationId);
        bool ReferPatient(int patientId, int referringDocId, int referredDocId);
    }
}
