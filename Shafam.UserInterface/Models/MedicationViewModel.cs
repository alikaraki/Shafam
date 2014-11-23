using Shafam.Common.DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shafam.UserInterface.Models
{
    public class MedicationViewModel
    {
        public Patient Patient { get; set; }
        public List<SingleMedicationModel> Medications { get; set; }

    }
}