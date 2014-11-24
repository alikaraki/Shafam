namespace Shafam.Common.DataModel
{
    public class Referral
    {
        public int ReferralId { get; set; }
        public int ReferringDoctorId { get; set; }
        public int ReferredDoctorId { get; set; }
        public int PatientId { get; set; }
        public bool SeenByReferredDoctor { get; set; }
        public string Notes { get; set; }
    }
}
