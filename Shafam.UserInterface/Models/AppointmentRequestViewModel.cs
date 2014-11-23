using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shafam.Common.DataModel;
using System.ComponentModel;

namespace Shafam.UserInterface.Models
{
    public class AppointmentRequestViewModel
    {
        public Patient Patient { get; set; }
        [DisplayName("Patient's ID")]
        public int PatientId { get; set; }
        [DisplayName("Reason for Appointment")]
        public string Reason { get; set; }
    }
}