using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class DoctorAssignmentViewModel
    {
        private static List<string> _names;
        private static List<Doctor> _doctors;

        public Patient Patient { get; set; }
        [DisplayName("Select Doctor")]
        public Doctor AssignedDoctor { get; set; }
        public List<Doctor> Doctors
        { 
            get 
            {
                return _doctors;
            } 
            set
            {
                _doctors = value;
            }
        }
        public List<string> DoctorNames
        {
            get
            {
                return GetDoctorNames();
            }
        }

        private static List<string> GetDoctorNames()
        {
            foreach (Doctor doctor in _doctors)
            {
                _names.Add(doctor.LastName + ", " + doctor.FirstName);
            }
            return _names;
        }
    }
}