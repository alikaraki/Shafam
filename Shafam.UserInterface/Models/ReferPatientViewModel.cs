﻿using System.Collections.Generic;
using System.ComponentModel;
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
                var items = new List<SelectListItem>();
                var itemsDict = new Dictionary<string, int>();

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
    }
}