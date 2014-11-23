using Shafam.Common.DataModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shafam.UserInterface.Models
{
    public class TestViewModel
    {
        public Patient Patient { get; set; }
        public List<SingleTestModel> Tests { get; set; }

    }
}