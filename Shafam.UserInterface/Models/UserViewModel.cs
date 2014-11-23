using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Shafam.Common.DataModel;

namespace Shafam.UserInterface.Models
{
    public class UserViewModel
    {
        [Key]
        [Display(Name = "ID")]
        public int UserViewModelId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Speciality")]
        public string Speciality { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Role")]
        public UserRole UserRole { get; set; }

        [Display(Name = "Role")]
        public string SelectedRole { get; set; }

        [Display(Name = "Account Status")]
        public string AccountStatus { get; set; }

        public IEnumerable<SelectListItem> Roles
        {
            get
            {
                return new List<SelectListItem>
                       {
                           new SelectListItem {Value = "Doctor", Text = "Doctor"},
                           new SelectListItem {Value = "Legal", Text = "Legal"},
                           new SelectListItem {Value = "Staff", Text = "Staff"},
                           new SelectListItem {Value = "Finance", Text = "Finance"},
                           new SelectListItem {Value = "IT", Text = "IT"},
                       };
            }
        }

        public IEnumerable<SelectListItem> Departments
        {
            get
            {
                return new List<SelectListItem>
                       {
                           new SelectListItem {Value = "Surgery", Text = "Surgery"},
                           new SelectListItem {Value = "Dentistry", Text = "Dentistry"},
                           new SelectListItem {Value = "Physiology", Text = "Physiology"},
                           new SelectListItem {Value = "Cardiology", Text = "Cardiology"},
                           new SelectListItem {Value = "EarNoseThroat", Text = "EarNoseThroat"},
                           new SelectListItem {Value = "Neurology", Text = "Neurology"},
                       };
            }
        }

        public UserRole GetRole()
        {
            switch (SelectedRole)
            {
                case "Doctor":
                    return UserRole.Doctor;

                case "Legal":
                    return UserRole.Legal;

                case "Staff":
                    return UserRole.Staff;

                case "Finance":
                    return UserRole.Finance;

                default:
                    return UserRole.IT;
            }
        }
    }
}