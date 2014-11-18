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

        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int VisitationId { get; set; }
    }
}