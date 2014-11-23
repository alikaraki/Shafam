using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class DoctorAssignmentViewModel
    {
        public Patient Patient { get; set; }

        [DisplayName("Select Doctor")]
        public string AssignedDoctorId { get; set; }

        public List<Doctor> Doctors { get; set; }

        public IEnumerable<SelectListItem> DoctorList
        {
            get
            {
                var items = new List<SelectListItem>();

                foreach (Doctor doctor in Doctors)
                {
                    items.Add(new SelectListItem
                              {
                                  Text = doctor.LastName + ", " + doctor.FirstName,
                                  Value = doctor.DoctorId.ToString()
                              });
                }

                return items;
            }
        }
    }
}