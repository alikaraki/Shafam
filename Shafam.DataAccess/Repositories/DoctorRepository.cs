using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}