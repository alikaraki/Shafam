using Shafam.Common.DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shafam.UserInterface.Models
{
    public class TreatmentViewModel
    {
        public Patient Patient { get; set; }
        public List<SingleTreatmentModel> Treatments { get; set; }

    }
}