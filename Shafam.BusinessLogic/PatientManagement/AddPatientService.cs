using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic.PatientManagement
{
    class AddPatientService
    {
        private Patient patient;

        int AddPatient(Patient patient, int doctorId)
        {
            int patientId = 1;
            return patientId;
        }

        int GetPatientId(Patient patient)
        {
            int patientId = 1;
            return patientId;
        }

        string GetPatientUsername(Patient patient)
        {
            return "Username";
        }

        string GetPatientPassword(Patient patient)
        {
            return "Password";
        }

        int AssignDoctor(int patientId)
        {
            int doctorId = 1;
            return doctorId;
        }
    }
}
