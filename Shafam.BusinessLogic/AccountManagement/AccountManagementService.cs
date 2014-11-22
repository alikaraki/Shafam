using System;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.AccountManagement
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountManagementService(IDoctorRepository doctorRepository,
                                        IPatientRepository patientRepository,
                                        IStaffRepository staffRepository,
                                        IAccountRepository accountRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _staffRepository = staffRepository;
            _accountRepository = accountRepository;
        }

        public string CreateAccountForPatient(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);

            string username = GenerateUsername(patient.FirstName);
            return CreateAccount(username, UserRole.Patient, patientId).Username;
        }

        public string CreateAccountForDoctor(int doctorId)
        {
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);

            string username = GenerateUsername(doctor.FirstName);
            return CreateAccount(username, UserRole.Doctor, doctorId).Username;
        }

        public string CreateAccountForStaff(int staffId, UserRole role)
        {
            Staff staff = _staffRepository.GetStaff(staffId);

            string username = GenerateUsername(staff.FirstName);
            return CreateAccount(username, role, staffId).Username;
        }

        private string GenerateUsername(string suggestedUsername)
        {
            string username = string.IsNullOrEmpty(suggestedUsername) ? "user" : suggestedUsername;
            int postfix = 1;

            while (_accountRepository.GetAccount(username) != null)
            {
                username += postfix++;
            }

            return username;
        }

        private Account CreateAccount(string username, UserRole role, int userId)
        {
            var account = new Account
                          {
                              Username = username,
                              Password = username,
                              Role = role,
                              UserId = userId
                          };

            _accountRepository.CreateAccount(account);
            return account;
        }
    }
}