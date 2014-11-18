using System.Collections.Generic;

namespace Shafam.Common.DataModel
{
    public class Doctor : Staff
    {
        public string Speciality { get; set; }
        public Gender Gender { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Visitation> Visitations { get; set; }
    }
}