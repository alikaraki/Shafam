using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class DoctorPatientViewModel
    {
        public List<Patient> Patients { get; set; }
        public List<Doctor> Doctors { get; set; }

        // drop-down list for Doctors 
        public IEnumerable<SelectListItem> DoctorList
        {
            get
            {
                var items = new List<SelectListItem>();                
                var itemsDict = new Dictionary<string,int>();

                foreach (Doctor doctor in Doctors)                
                    itemsDict.Add(doctor.LastName + ", " + doctor.FirstName, doctor.DoctorId);                

                var sortedDocNames = new List<string>(itemsDict.Keys);
                sortedDocNames.Sort();

                foreach (string docName in sortedDocNames)
                {
                    int docId;
                    if (itemsDict.TryGetValue(docName, out docId))
                    {
                        items.Add(new SelectListItem
                        {
                            Text = docName,
                            Value = docId.ToString()
                        });
                    }
                }

                return items;
            }
        }
        // drop-down list for Patients 
        public IEnumerable<SelectListItem> PatientList
        {
            get
            {
                var items = new List<SelectListItem>();
                var itemsDict = new Dictionary<string, int>();

                foreach (Patient patient in Patients)
                    itemsDict.Add(patient.LastName + ", " + patient.FirstName, patient.PatientId);

                var sortedPatNames = new List<string>(itemsDict.Keys);
                sortedPatNames.Sort();

                foreach (string patName in sortedPatNames)
                {
                    int patId;
                    if (itemsDict.TryGetValue(patName, out patId))
                    {
                        items.Add(new SelectListItem
                        {
                            Text = patName,
                            Value = patId.ToString()
                        });
                    }
                }

                return items;
            }
        }
    }
}