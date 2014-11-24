<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.UserInterface.Infrastructure;
using Shafam.BusinessLogic;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Finance)]
    public class FinanceController : Controller
    {
        private readonly IBillingManagementService _billingManagementService;
        private readonly IPatientManagementService _patientManagementService;
        private readonly IDoctorRepository _doctorRepository;

        
        public FinanceController(IBillingManagementService billingManagementService,
                                    IPatientManagementService patientManagementService,
                                    IDoctorRepository doctorRepository)
        {
            _billingManagementService = billingManagementService;
            _patientManagementService = patientManagementService;
            _doctorRepository = doctorRepository;
        }
        
        
        public ActionResult Index()
        {
            return RedirectToAction("Doctors");
        }


        public ActionResult Doctors()
        {
            IEnumerable<Doctor> doctors = _doctorRepository.GetDoctors();
            return View(doctors);
        }


        public ActionResult Patients()
        {
            List<Patient> patients = _patientManagementService.AllPatients();
            return View(patients);
        }


        public ActionResult TimeBill()
        {
            return View();
        }


	}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface.Controllers
{
    [Authorize(Roles = UserRoles.Finance)]
    public class FinanceController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Bills");
        }

        public ActionResult Bills()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Doctors()
        {
            return View();
        }

        public ActionResult Patients()
        {
            return View();
        }
	}
>>>>>>> d29842eb0b6bbf8ab931708e265336d3e26ff5d8
}