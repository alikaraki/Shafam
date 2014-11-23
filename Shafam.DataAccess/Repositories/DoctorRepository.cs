using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IShafamDataContext _dataContext;

        public DoctorRepository(IShafamDataContext shafamDataContext)
        {
            _dataContext = shafamDataContext;
        }

        public void AddDoctor(Doctor doctor)
        {
            _dataContext.Doctors.Add(doctor);
            _dataContext.Save();
        }

        public List<Doctor> GetDoctors()
        {
            return _dataContext.Doctors.ToList();
        }

        public Doctor GetDoctor(int doctorId)
        {
            return _dataContext.Doctors.First(d => d.DoctorId == doctorId);
        }

        public IEnumerable<Doctor> GetDoctorsInDepartment(Department department)
        {
            return _dataContext.Doctors.Where(d => d.Department == department).ToList();
        }

        public void UpdateDoctor(Doctor updatedDoctor)
        {
            Doctor doctor = GetDoctor(updatedDoctor.DoctorId);
            doctor.FirstName = updatedDoctor.FirstName;
            doctor.LastName = updatedDoctor.LastName;

            _dataContext.Save();
        }

        public void DeleteDoctor(int doctorId)
        {
            Doctor doctor = GetDoctor(doctorId);
            _dataContext.Doctors.Remove(doctor);
            _dataContext.Save();
        }

        public void AssignPatient(int doctorId, int patientId)
        {
            Doctor doctor = GetDoctor(doctorId);
            Patient patient = _dataContext.Patients.First(p => p.PatientId == patientId);
            doctor.Patients.Add(patient);
            _dataContext.Save();
        }

        public List<Patient> GetPatientsForDoctor(int doctorId)
        {
            Doctor doctor = GetDoctor(doctorId);
            return doctor.Patients.ToList();
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            Doctor doctor = GetDoctor(doctorId);
            return doctor.Appointments.ToList();
        }
    }
}