using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class StaffHomeViewModel
    {
        public IEnumerable<Patient> Patients { get; set; } 
        public IEnumerable<Notification> Notifications { get; set; } 
    }
}