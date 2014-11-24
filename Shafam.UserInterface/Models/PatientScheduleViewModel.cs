using System.Collections.Generic;
using Shafam.Common.DataModel;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Models
{
    public class PatientScheduleViewModel
    {
        public Patient Patient { get; set; }
        public List<SingleAppointmentViewModel> SingleAppointmentViewModels { get; set; }
    }
}