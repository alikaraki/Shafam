using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);

        List<Patient> GetPatients();

        Patient GetPatient(int patientid);

        List<Doctor> GetDoctorsForPatient(int patientId);

        void DeletePatient(int patientId);

        IEnumerable<Patient> GetUnassignedPatients();
    }
}