using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IShafamDataContext _context;

        public PatientRepository(IShafamDataContext context)
        {
            _context = context;
        }

        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.Save();
        }

        public List<Patient> GetPatients()
        {
            List<Patient> patients = _context.Patients.ToList();
            //patients = patients.OrderBy(p => p.LastName).ToList();
            return patients;
        }

        public Patient GetPatient(int patientid)
        {
            return _context.Patients.First(p => p.PatientId == patientid);
        }

        public List<Doctor> GetDoctorsForPatient(int patientId)
        {
            Patient patient = GetPatient(patientId);
            return patient.Doctors.ToList();
        }

        public void DeletePatient(int patientId)
        {
            Patient patient = GetPatient(patientId);
            _context.Patients.Remove(patient);
        }

        public IEnumerable<Patient> GetUnassignedPatients()
        {
            return _context.Patients.Where(p => !p.Doctors.Any()).ToList();
        }
    }
}