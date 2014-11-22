using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Shafam.BusinessLogic;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.UserInterface.Infrastructure;
using Shafam.UserInterface.Models;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.IT)]
    public class ITController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountManagementService _accountManagementService;

        public ITController(IDoctorRepository doctorRepository,
                            IStaffRepository staffRepository,
                            IAccountRepository accountRepository,
                            IAccountManagementService accountManagementService)
        {
            _doctorRepository = doctorRepository;
            _staffRepository = staffRepository;
            _accountRepository = accountRepository;
            _accountManagementService = accountManagementService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Users");
        }
        
        public ActionResult Users()
        {
            return View(GetDoctors().Concat(GetStaff()));
        }

        public ActionResult Details(int id, UserRole role, bool showUserCreatedAlert = false, bool showEnabledDisabledAlert = false)
        {
            if (showUserCreatedAlert)
            {
                ViewBag.ShowUserCreatedAlert = true;
            }

            if (showEnabledDisabledAlert)
            {
                ViewBag.ShowEnabledDisabledAlert = true;
            }

            if (role == UserRole.Doctor)
            {
                return View(GetDoctor(id));
            }

            return View(GetStaff(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public ActionResult Create(UserViewModel viewModel)
        {
            UserRole role = viewModel.GetRole();
            int id;

            if (role == UserRole.Doctor)
            {
                Doctor doctor = viewModel.GetDoctor();
                _doctorRepository.AddDoctor(doctor);
                
                id = doctor.DoctorId;
                _accountManagementService.CreateAccountForDoctor(id);

            }
            else
            {
                Staff staff = viewModel.GetStaff();
                _staffRepository.AddStaff(staff);

                id = staff.StaffId;
                _accountManagementService.CreateAccountForStaff(id, role);   
            }

            return RedirectToAction("Details", new {id, role, showUserCreatedAlert = true});
        }

        public ActionResult Disable(int id, UserRole role)
        {
            _accountRepository.DisableAccount(id);
            return RedirectToAction("Details", new { id, role, showEnabledDisabledAlert = true});
        }

        public ActionResult Enable(int id, UserRole role)
        {
            _accountRepository.EnableAccount(id);
            return RedirectToAction("Details", new { id, role, showEnabledDisabledAlert = true });
        }

        private IEnumerable<UserViewModel> GetDoctors()
        {
            var doctors = _doctorRepository.GetDoctors();
            return doctors.Select(d => GetDoctor(d.DoctorId));
        }

        private IEnumerable<UserViewModel> GetStaff()
        {
            var staff = _staffRepository.GetAllStaff();
            return staff.Select(s => GetStaff(s.StaffId));
        }
            
        private UserViewModel GetDoctor(int id)
        {
            var doctor = _doctorRepository.GetDoctor(id);
            return doctor.GetUserViewModel(_accountRepository.GetAccountByUserId(doctor.DoctorId));
        }
        
        private UserViewModel GetStaff(int id)
        {
            var staff = _staffRepository.GetStaff(id);
            return staff.GetUserViewModel(_accountRepository.GetAccountByUserId(staff.StaffId));
        }
    }
}