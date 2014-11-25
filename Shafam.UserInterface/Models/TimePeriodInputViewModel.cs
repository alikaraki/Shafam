using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shafam.Common.DataModel;
using System.ComponentModel;

namespace Shafam.UserInterface.Models
{
    public class TimePeriodInputViewModel
    {
        [DisplayName("Start Date")]
        public DateTime Begin { get; set; }
        [DisplayName("End Date")]
        public DateTime End { get; set; }

    }
}