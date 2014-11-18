using System;

namespace Shafam.Common.DataModel
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime DateTime { get; set; }  
        public string Reason { get; set; }
    }
}