using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel

namespace Shafam.BusinessLogic
{
    interface IBillingManagementService
    {
        Bill GenerateDoctorBill(int doctorid);
        Bill GenerateTimePeriodBill (DateTime start, DateTime end);
        
        bool SendBill(Bill bill, string emailAddress);
    }
}
