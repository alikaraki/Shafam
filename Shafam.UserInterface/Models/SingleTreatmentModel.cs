using System;
using Shafam.Common.DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shafam.UserInterface.Models
{
    public class SingleTreatmentModel
    {
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Treatment Treatment { get; set; }
    }
}