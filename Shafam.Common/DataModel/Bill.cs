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

        public List<Visitation> Visitations { get; set; }
    }
}
