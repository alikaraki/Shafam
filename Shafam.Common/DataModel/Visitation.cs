using System.Collections.Generic;

namespace Shafam.Common.DataModel
{
    public class Visitation : Appointment
    {
        public int VisitationId { get; set; }
        public string Notes { get; set; }

        public ICollection<Medication> Medications { get; set; } 
        public ICollection<Test> Tests { get; set; } 
        public ICollection<Treatment> Treatments { get; set; } 
    }
}