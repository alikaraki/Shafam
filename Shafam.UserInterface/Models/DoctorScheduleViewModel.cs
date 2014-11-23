using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class DoctorScheduleViewModel
    {
        public Doctor Doctor { get; set; }
        public List<Appointment> Appoitnemnts { get; set; }
    }
}