using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface IBillingManagementService
    {
        Bill GenerateBill(int doctorid);
        Bill GenerateBill (DateTime start, DateTime end);
        //Bill GenerateBill (Department DepartmentId);
        
        bool SendBill(Bill bill, string emailAddress);
    }
}
