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

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Medication> Medications { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Staff> Staffs { get; set; }

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