using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.DataAccess
{
    public interface IStaffRepository
    {
        void AddStaff(Staff staff);

        Staff GetStaff(int staffId);

        List<Staff> GetAllStaff();
        
        List<Staff> GetDepartmentStaff(Department department);

        void DeleteStaff(int staffIdId);

        void UpdateStaff(Staff updatedStaff);

    }
}
