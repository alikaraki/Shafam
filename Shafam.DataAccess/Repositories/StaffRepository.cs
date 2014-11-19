using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly IShafamDataContext _dataContext;

        public StaffRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddStaff(Staff staff)
        {
            _dataContext.Staffs.Add(staff);
            _dataContext.Save();
        }

        public Staff GetStaff(int staffId)
        {
            return _dataContext.Staffs.First(s => s.StaffId == staffId);
        }

        public List<Staff> GetDepartmentStaff(string department)
        {
            return _dataContext.Staffs.Where(s => s.Department == department).ToList();
        }

        public void DeleteStaff(int staffId)
        {
            Staff staff = GetStaff(staffId);
            _dataContext.Staffs.Remove(staff);
            _dataContext.Save();
        }

        public void UpdateStaff(Staff updatedStaff)
        {
            Staff staff = GetStaff(updatedStaff.StaffId);
            staff.FirstName = updatedStaff.FirstName;
            staff.LastName = updatedStaff.LastName;

            _dataContext.Save();
        }
    }
}
