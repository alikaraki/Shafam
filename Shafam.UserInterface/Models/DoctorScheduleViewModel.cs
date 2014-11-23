using Shafam.Common.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shafam.UserInterface.Models
{
    public class DoctorScheduleViewModel
    {
        public Doctor Doctor { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}