using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class PatientViewModel
    {
        public List<Patient> Patients { get; set; }
        public Doctor Doctor { get; set; }
    }
}