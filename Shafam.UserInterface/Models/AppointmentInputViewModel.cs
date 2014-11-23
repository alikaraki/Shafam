using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shafam.Common.DataModel;
using System.ComponentModel;

namespace Shafam.UserInterface.Models
{
    public class AppointmentInputViewModel
    {
        public Doctor Doctor { get; set; }
        [DisplayName("Patient's ID")]
        public int PatientID { get; set; }
        [DisplayName("Reason for Appointment")]
        public string Reason { get; set; }
        [DisplayName("Appointment Date and Time")]
        public DateTime DateTime { get; set; }
    }
}