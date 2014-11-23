using System;
using Shafam.Common.DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shafam.UserInterface.Models
{
    public class VisitationDetailModel
    {
        public Patient Patient { get; set; }
        public Visitation Visitation { get; set; }
        public Medication Medication { get; set; }
        public Treatment Treatment { get; set; }
        public Test Test { get; set; }
    }
}