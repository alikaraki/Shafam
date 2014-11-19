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

        public IDbSet<User> Users { get; set; }

        public IDbSet<Medication> Medications { get; set; }

        public IDbSet<Treatment> Treatments { get; set; }

        public IDbSet<Test> Tests { get; set; }

        public IDbSet<Appointment> Appointments { get; set; }

        public IDbSet<AppointmentRequest> AppointmentRequests { get; set; }

        public IDbSet<Visitation> Visitations { get; set; }

        public IDbSet<Staff> Staff { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
                        .HasKey(u => u.UserId)
                        .Map(m => m.MapInheritedProperties()).ToTable("User")
                        .Property(p => p.UserId)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Patient>()
                        .HasKey(u => u.UserId)
                        .Map(m => m.MapInheritedProperties()).ToTable("Patient");

            modelBuilder.Entity<Doctor>()
                        .HasKey(u => u.UserId)
                        .Map(m => m.MapInheritedProperties()).ToTable("Doctor");

            modelBuilder.Entity<Staff>()
                        .HasKey(u => u.UserId)
                        .Map(m => m.MapInheritedProperties()).ToTable("Staff");
        }

        public void Save()
        {
            base.SaveChanges();
        }
    }
}