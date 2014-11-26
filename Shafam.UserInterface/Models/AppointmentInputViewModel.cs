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
        [DisplayName("Patient's ID")]
        public int PatientID { get; set; }
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
                var items = new List<SelectListItem>();
                var itemsDict = new Dictionary<string, int>();

                foreach (Patient patient in Patients)
                    itemsDict.Add(patient.LastName + ", " + patient.FirstName, patient.PatientId);

                var sortedPatientNames = new List<string>(itemsDict.Keys);
                sortedPatientNames.Sort();

                foreach (string patientName in sortedPatientNames)
                {
                    int patientId;
                    if (itemsDict.TryGetValue(patientName, out patientId))
                    {
                        items.Add(new SelectListItem
                        {
                            Text = patientName,
                            Value = patientId.ToString()
                        });
                    }
                }

                return items;
            }
        }
    }
}