using System.Data.Entity;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess.Infrastructure
{
    public interface IShafamDataContext
    {
        DbSet<Patient> Patients { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Medication> Medications { get; set; }
        DbSet<Treatment> Treatments { get; set; }
        DbSet<Test> Tests { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        DbSet<Visitation> Visitations { get; set; }
        DbSet<Staff> Staffs { get; set; } 

        void Save();
    }
}
