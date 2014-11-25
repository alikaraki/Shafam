using System;
using System.Collections.Generic;

namespace Shafam.Common.DataModel
{
    public class Visitation
    {
        public int VisitationId { get; set; }
        public string Notes { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public double Rate { get { return 100.00; } }   // flat rate of $100 for each visitation to any doctor

        public ICollection<Medication> Medications { get; set; } 
        public ICollection<Test> Tests { get; set; } 
        public ICollection<Treatment> Treatments { get; set; } 
    }
}