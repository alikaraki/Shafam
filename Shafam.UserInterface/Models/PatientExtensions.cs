using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public static class PatientExtensions
    {
        public static UserViewModel GetUserViewModel(this Patient patient, Account account)
        {
            if (patient == null)
            {
                return null;
            }

            var userViewModel = new UserViewModel
            {
                UserViewModelId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber                 
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