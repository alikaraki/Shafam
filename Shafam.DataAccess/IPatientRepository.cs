using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);
        IEnumerable<Patient> GetPatients();
        Patient GetPatient(int patientid);
    }
}