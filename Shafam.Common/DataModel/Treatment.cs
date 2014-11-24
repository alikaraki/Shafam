namespace Shafam.Common.DataModel
{
    public class Treatment
    {
        public int TreatmentId { get; set; }
        public string TreatmentType { get; set; }
        public float Rate { get; set; }
        public bool TreatmentCompleted { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int VisitationId { get; set; }
    }
}
