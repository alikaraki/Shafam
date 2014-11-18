using System.Data.Entity;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess.Infrastructure
{
    public class ShafamDataContext : DbContext, IShafamDataContext
    {
        public ShafamDataContext()
            : base("ShafamConnectionString")
        {
        }

        public IDbSet<Patient> Patients { get; set; }

        public IDbSet<Doctor> Doctors { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Medication> Medications { get; set; }

        public IDbSet<Treatment> Treatments { get; set; }

        public IDbSet<Test> Tests { get; set; }

        public IDbSet<Appointment> Appointments { get; set; }

        public IDbSet<AppointmentRequest> AppointmentRequests { get; set; }

        public IDbSet<Visitation> Visitations { get; set; }

        public IDbSet<Staff> Staff { get; set; } 

        public void Save()
        {
            base.SaveChanges();
        }
    }
}