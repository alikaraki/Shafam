using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Shafam.Common.DataModel;
using System.ComponentModel;

namespace Shafam.UserInterface.Models
{
    public class AppointmentInputViewModel
    {
        public Doctor Doctor { get; set; }
        [DisplayName("Reason for Appointment")]
        public string Reason { get; set; }
        [DisplayName("Appointment Date and Time")]
        public DateTime DateTime { get; set; }

        [DisplayName("Select Patient")]
        public string AssignedPatientId { get; set; }

        public List<Patient> Patients { get; set; }

        // drop-down list for Patients 
        public IEnumerable<SelectListItem> PatientList
        {
            get
            {
                IEnumerable<SelectListItem> items = Patients.Select(p =>
                                                                        new SelectListItem
                                                                        {
                                                                            Text = p.LastName + ", " + p.FirstName,
                                                                            Value = p.PatientId.ToString()
                                                                        });

                items.ToList().Sort((item1, item2) => item1.Text.CompareTo(item2.Text));

                return items;
            }
        }
    }
}