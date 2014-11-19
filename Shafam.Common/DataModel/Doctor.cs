using System.Collections.Generic;

namespace Shafam.Common.DataModel
{
    public class Doctor
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }

        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string Speciality { get; set; }
        public Gender Gender { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Visitation> Visitations { get; set; }
    }
}