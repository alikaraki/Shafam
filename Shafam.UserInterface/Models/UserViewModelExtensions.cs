using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public static class UserViewModelExtensions
    {
        public static Doctor GetDoctor(this UserViewModel viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }

            return new Doctor
                   {
                       FirstName = viewModel.FirstName,
                       LastName = viewModel.LastName,
                       Address = viewModel.Address,
                       PhoneNumber = viewModel.PhoneNumber,
                       Specialty = viewModel.SpecialtyDepartment,
                   };
        }        
        
        public static Staff GetStaff(this UserViewModel viewModel)
        {
            if (viewModel == null)
            {
                return null;
            }

            return new Staff
                   {
                       FirstName = viewModel.FirstName,
                       LastName = viewModel.LastName,
                       Address = viewModel.Address,
                       PhoneNumber = viewModel.PhoneNumber,
                       Department = viewModel.SpecialtyDepartment,
                   };
        }
    }
}