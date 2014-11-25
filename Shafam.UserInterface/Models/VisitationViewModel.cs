using Shafam.Common.DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shafam.UserInterface.Models
{
    public class VisitationViewModel
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public IEnumerable<Visitation> Visitations { get; set; }
        public List<Doctor> DoctorList { get; set; }
    }
}