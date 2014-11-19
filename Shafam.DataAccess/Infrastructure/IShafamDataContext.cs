using System.Data.Entity;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess.Infrastructure
{
    public interface IShafamDataContext
    {
        IDbSet<Patient> Patients { get; set; }
        IDbSet<Doctor> Doctors { get; set; }
        IDbSet<Account> Accounts { get; set; }
        IDbSet<Medication> Medications { get; set; }
        IDbSet<Treatment> Treatments { get; set; }
        IDbSet<Test> Tests { get; set; }
        IDbSet<Appointment> Appointments { get; set; }
        IDbSet<AppointmentRequest> AppointmentRequests { get; set; }
        IDbSet<Visitation> Visitations { get; set; }
        IDbSet<Staff> Staffs { get; set; } 

        void Save();
    }
}
