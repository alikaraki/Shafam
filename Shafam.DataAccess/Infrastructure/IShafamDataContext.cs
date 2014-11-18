using System.Data.Entity;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess.Infrastructure
{
    public interface IShafamDataContext
    {
        IDbSet<Patient> Patients { get; }
        IDbSet<Doctor> Doctors { get; } 
        IDbSet<User> Users { get; }
        IDbSet<Medication> Medications { get; set; }
        IDbSet<Treatment> Treatments { get; set; }
        IDbSet<Test> Tests { get; set; }
        IDbSet<Appointment> Appointments { get; set; }
        IDbSet<AppointmentRequest> AppointmentRequests { get; set; }
        IDbSet<Visitation> Visitations { get; set; }
        IDbSet<Staff> Staff { get; set; } 

        void Save();
    }
}
