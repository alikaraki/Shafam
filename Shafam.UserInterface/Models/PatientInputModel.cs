using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class PatientInputModel
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Phone Name")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        [DisplayName("Health Card Number")]
        public string HealthCardNumber { get; set; }
    }
}
