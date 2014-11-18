namespace Shafam.Common.DataModel
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Instructions { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int VisitationId { get; set; }
    }
}