using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.UserInterface.Models;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _accountRepository.VerifyAccount(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);

                    switch (user.Role)
                    {
                        case UserRole.Doctor:
                            return RedirectToAction("Index", "Doctor");

                        case UserRole.IT:
                            return RedirectToAction("Index", "IT");

                        case UserRole.Finance:
                            return RedirectToAction("Index", "Finance");

                        case UserRole.Legal:
                            return RedirectToAction("Index", "Legal");

                        case UserRole.Staff:
                            return RedirectToAction("Index", "Staff");

                        case UserRole.Patient:
                            return RedirectToAction("Index", "Patient");

                        default:
                            return RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LoginAsDoctorAmy(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("Amy");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Doctor");
        }

        [AllowAnonymous]
        public ActionResult LoginAsDoctorJohn(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("John");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Doctor");
        }

        [AllowAnonymous]
        public ActionResult LoginAsStaffMichael(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("Michael");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Staff");
        }

        [AllowAnonymous]
        public ActionResult LoginAsStaffSara(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("Sara");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Staff");
        }

        [AllowAnonymous]
        public ActionResult LoginAsPatient(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("Patient");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Patient");
        }

        [AllowAnonymous]
        public ActionResult LoginAsStaff(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("Staff");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Staff");
        }

        [AllowAnonymous]
        public ActionResult LoginAsIT(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("IT");
            SignInAsync(account, true);

            return RedirectToAction("Index", "IT");
        }

        [AllowAnonymous]
        public ActionResult LoginAsLegal(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("Legal");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Legal");
        }

        [AllowAnonymous]
        public ActionResult LoginAsFinance(string returnUrl)
        {
            Account account = _accountRepository.GetAccount("Finance");
            SignInAsync(account, true);

            return RedirectToAction("Index", "Finance");
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                if (_accountRepository.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                }

                AddErrors(new IdentityResult(new []{ "Could not change password. Please verify your data." }));
            }

            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(Account account, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            ClaimsIdentity identity = CreateIdentity(account);

            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private ClaimsIdentity CreateIdentity(Account account)
        {
            var nameClaim = new Claim(ClaimTypes.Name, account.Username);
            var userIdClaim = new Claim(ClaimTypes.NameIdentifier, account.Username);
            var roleClaim = new Claim(ClaimTypes.Role, account.Role.ToString());
            var claims = new List<Claim> { nameClaim, userIdClaim, roleClaim };

            return new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
                
            return RedirectToAction("Index", "Home");
        }
    }
}