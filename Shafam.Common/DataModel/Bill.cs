using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shafam.Common.DataModel
{
    public class Bill
    {
        public int BillId { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}