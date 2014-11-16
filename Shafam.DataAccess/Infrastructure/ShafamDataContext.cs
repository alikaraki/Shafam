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

        public void Save()
        {
            base.SaveChanges();
        }
    }
}