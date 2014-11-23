using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public static class DoctorExtensions
    {
        public static UserViewModel GetUserViewModel(this Doctor doctor, Account account)
        {
            if (doctor == null)
            {
                return null;
            }

            var userViewModel = new UserViewModel
            {
                UserViewModelId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Address = doctor.Address,
                PhoneNumber = doctor.PhoneNumber,
                Speciality = doctor.Specialty,
                Department = doctor.Department.ToString()
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