using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class TimeBillViewModel
    {
        public int NumberOfVisitations { get; set; }
        public Dictionary<string, int> TestDict { get; set; }
        public Dictionary<string, int> TreatmentDict { get; set; }
        public Dictionary<string, int> MedicationDict { get; set; }

    }
}