using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class ReferPatientViewModel
    {
        public Patient Patient { get; set; }
        public Referral Referral { get; set; }

        public string ReferringDoctorId { get; set; }
        
        [DisplayName("Refer to Doctor")]
        public string ReferredDoctorId { get; set; }

        public List<Doctor> Doctors { get; set; }
        public List<Doctor> ReferredDoctors { get; set; }

        // drop-down list for Doctors 
        public IEnumerable<SelectListItem> DoctorList
        {
            get
            {
                IEnumerable<SelectListItem> items = Doctors.Select(d =>
                    new SelectListItem
                    {
                        Text = d.LastName + ", " + d.FirstName,
                        Value = d.DoctorId.ToString()
                    });

                items.ToList().Sort((item1, item2) => item1.Text.CompareTo(item2.Text));

                return items;
            }
        }
    }
}