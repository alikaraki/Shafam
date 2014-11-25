namespace Shafam.Common.DataModel
{
    public class Treatment
    {
        public int TreatmentId { get; set; }
        public string TreatmentType { get; set; }
        public double Rate 
        { 
            get
            {
                if (!string.IsNullOrEmpty(TreatmentType))
                {
                    if (TreatmentType.Equals("Stitches"))
                        return 100.00;
                    else if (TreatmentType.Equals("Cast"))
                        return 200.00;
                }
                return 50.00;   //flat rate of $50
            }
        }
        public bool TreatmentCompleted { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int VisitationId { get; set; }
    }
}
