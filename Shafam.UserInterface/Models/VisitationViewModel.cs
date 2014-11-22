﻿using Shafam.Common.DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shafam.UserInterface.Models
{
    public class VisitationViewModel
    {
        public Patient Patient { get; set; }
        public IEnumerable<Visitation> Visitations { get; set; }
    }
}