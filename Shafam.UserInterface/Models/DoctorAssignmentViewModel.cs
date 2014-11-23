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
        public List<Doctor> AssignedDoctors { get; set; }

        // drop-down list for Doctors 
        public IEnumerable<SelectListItem> DoctorList
        {
            get
            {
                //var items = new List<SelectListItem>();
                //var itemsText = new List<string>();
                var itemsDict = new Dictionary<string,int>();

                foreach (Doctor doctor in Doctors)
                {
                    //itemsText.Add(doctor.LastName + ", " + doctor.FirstName);
                    itemsDict.Add(doctor.LastName + ", " + doctor.FirstName, doctor.DoctorId);

                    /*items.Add(new SelectListItem
                              {
                                  Text = doctor.LastName + ", " + doctor.FirstName,
                                  Value = doctor.DoctorId.ToString()
                              });
                     */
                }

                var itemsText = new List<string>(itemsDict.Keys);
                itemsText.Sort();
                
                var itemsSelectList = new SelectList(itemsText);

                foreach (SelectListItem item in itemsSelectList)
                {
                    int docId;
                    if (itemsDict.TryGetValue(item.Text, out docId))
                        item.Value = docId.ToString();
                }

                return itemsSelectList;
            }
        }
    }
}