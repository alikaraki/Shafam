using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class TestResultInputModel
    {
        public Patient Patient { get; set; }

        public Test Test { get; set; }

    }
}