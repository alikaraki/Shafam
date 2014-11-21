using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IDoctorRepository
    {
        void AddDoctor(Doctor doctor);

        List<Doctor> GetDoctors();

        Doctor GetDoctor(int doctorId);

        void UpdateDoctor(Doctor doctor);

        void DeleteDoctor(int doctorId);

        List<Patient> GetPatientsForDoctor(int doctorId);

        void AssignPatient(int doctorId, int patientId);

        List<Patient> GetPatientsForDoctor(int doctorId);
    }
}