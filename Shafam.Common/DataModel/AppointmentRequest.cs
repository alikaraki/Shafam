namespace Shafam.Common.DataModel
{
    public class AppointmentRequest
    {
        public int AppointmentRequestId { get; set; }
        public int? DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Reason { get; set; }
        public bool SeenByStaff { get; set; }
    }
}
