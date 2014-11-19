using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public IDbSet<Account> Accounts { get; set; }

        public IDbSet<Medication> Medications { get; set; }

        public IDbSet<Treatment> Treatments { get; set; }

        public IDbSet<Test> Tests { get; set; }

        public IDbSet<Appointment> Appointments { get; set; }

        public IDbSet<AppointmentRequest> AppointmentRequests { get; set; }

        public IDbSet<Visitation> Visitations { get; set; }

        public IDbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public void Save()
        {
            base.SaveChanges();
        }

    }
}