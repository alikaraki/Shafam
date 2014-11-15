using System.Data.Entity;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess.Infrastructure
{
    public interface IShafamDataContext
    {
        IDbSet<Patient> Patients { get; }

        IDbSet<Doctor> Doctors { get; } 

        void Save();
    }
}
