using System.Collections.Generic;

namespace Shafam.Common.DataModel
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string HealthCardNumber { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<AppointmentRequest> AppointmentRequests { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Visitation> Visitations { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }
    }
}