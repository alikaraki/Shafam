using System.Collections.Generic;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class PatientScheduleViewModel
    {
        public Patient Patient { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}