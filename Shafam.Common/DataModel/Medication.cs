namespace Shafam.Common.DataModel
{
    public class Medication
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Instructions { get; set; }
        public double Rate
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    if (Name.Equals("Advil"))
                        return 20.00;
                    else if (Name.Equals("TUMS"))
                        return 15.00;
                }
                return 10.00;   //flat rate of $10
            }
        }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int VisitationId { get; set; }
    }
}