using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class VisitationInputModel
    {
        public Patient Patient { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }

        [DisplayName("Medication Name")]
        public string MedicationName { get; set; }

        [DisplayName("Medication Quantity")]
        public string MedicationQuantity { get; set; }

        [DisplayName("Medication Instructions")]
        public string MedicationInstructions { get; set; }
        public string Treatment { get; set; }
        public string Test { get; set; }

    }
}