using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class DoctorBillViewModel
    {
        public Doctor Doctor {get; set; }
        public int NumberOfVisitations { get; set; }
        public double VisitationCost { get; set; }
        public Dictionary<string, Tuple<double, int, double>> TestDict { get; set; }
        public Dictionary<string, Tuple<double, int, double>> TreatmentDict { get; set; }
        public Dictionary<string, Tuple<double, int, double>> MedicationDict { get; set; }
        public double BillAmount { get; set; }
    }
}