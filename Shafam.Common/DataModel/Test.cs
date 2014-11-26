using System;

namespace Shafam.Common.DataModel
{
    public class Test
    {
        public int TestId { get; set; }
        public string Type { get; set; }
        public DateTime? Completed { get; set; }
        public string Result { get; set; }
        public bool SeenByDoctor { get; set; }
        public double Rate
        {
            get
            {
                if (!string.IsNullOrEmpty(Type))
                {
                    if (Type.Equals("X-Ray", StringComparison.InvariantCultureIgnoreCase))
                        return 150.00;
                    
                    if (Type.Equals("Blood Test", StringComparison.InvariantCultureIgnoreCase))
                        return 120.00;
                }
                return 100.00;   //flat rate of $100
            }
        }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int VisitationId { get; set; }
    }
}