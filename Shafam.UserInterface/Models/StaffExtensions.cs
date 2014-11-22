using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public static class StaffExtensions
    {
        public static UserViewModel GetUserViewModel(this Staff staff, Account account)
        {
            if (staff == null)
            {
                return null;
            }

            var userViewModel = new UserViewModel
                                {
                                    UserViewModelId = staff.StaffId,
                                    FirstName = staff.FirstName,
                                    LastName = staff.LastName,
                                    Address = staff.Address,
                                    PhoneNumber = staff.PhoneNumber,
                                    SpecialtyDepartment = staff.Department
                                };

            if (account != null)
            {
                userViewModel.UserRole = account.Role;
                userViewModel.Username = account.Username;
                userViewModel.AccountStatus = account.Disabled ? "Disabled" : "Enabled";
            }

            return userViewModel;
        }
    }
}