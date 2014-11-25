using System;
using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess;
using Shafam.BusinessLogic.NotificationManagement;

namespace Shafam.BusinessLogic.PatientManagement
{
    public class PatientManagementService : IPatientManagementService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IReferralRepository _referralRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="patientRepository"></param>
        /// <param name="staffRepository"></param>
        public PatientManagementService(IPatientRepository patientRepository,
                                        IDoctorRepository doctorRepository,
                                        IStaffRepository staffRepository,
                                        IReferralRepository referralRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _staffRepository = staffRepository;
            _referralRepository = referralRepository;
        }

        /// <summary>
        /// Gets a patient with specific patiendId from patient repository
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>Patient from the Patient Repository</returns>
        public Patient ViewPatient(int patientId)
        {
            return _patientRepository.GetPatient(patientId);
        }

        /// <summary>
        /// Returns list of Patients from Patient Repository for the doctor with doctorID in the Doctor Repository
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>list of Patients from Patient Repository for the doctor with doctorID in the Doctor Repository</returns>
        public List<Patient> ViewAllPatients(int doctorId)
        {
            return _doctorRepository.GetPatientsForDoctor(doctorId);
        }

        public List<Patient> AllPatients()
        {
            return _patientRepository.GetPatients();
        }

        public IEnumerable<Patient> ViewPatientsForStaff(int staffId)
        {
            var patients = new List<Patient>();
            patients.AddRange(_patientRepository.GetUnassignedPatients());

            Staff staff = _staffRepository.GetStaff(staffId);
            if (staff.Department.HasValue)
            {
                IEnumerable<Doctor> doctors = _doctorRepository.GetDoctorsInDepartment(staff.Department.Value);
                foreach (var doctor in doctors)
                {
                    patients.AddRange(_doctorRepository.GetPatientsForDoctor(doctor.DoctorId));
                }
            }

            return patients.GroupBy(p => p.PatientId).Select(grp => grp.First());
        }

        /// <summary>
        /// Adds patient to patient repository
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>patientID if successfully added, else invalue value '-1'</returns>
        public Patient AddPatient(string firstName, string lastName, int age, Gender gender, 
                            string healthCardNumber, string phoneNumber, string address)
        {
            var patient = new Patient
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Age = age,
                                Gender = gender,
                                HealthCardNumber = healthCardNumber,
                                PhoneNumber = phoneNumber,
                                Address = address
                            };
            _patientRepository.AddPatient(patient);
            return patient;
        }

        /// <summary>
        /// Adds Patient to specific doctor's list of patients
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="doctorId"></param>
        /// <returns>patientID if successfully added patient, else invalid value ‘-1’</returns>
        public Patient AddPatient(int doctorId, string firstName, string lastName, int age, Gender gender, 
                            string healthCardNumber, string phoneNumber, string address)
        {
            var patient = new Patient
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Age = age,
                                Gender = gender,
                                HealthCardNumber = healthCardNumber,
                                PhoneNumber = phoneNumber,
                                Address = address
                            };
            _patientRepository.AddPatient(patient);
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            _doctorRepository.AssignPatient(doctorId, patient.PatientId);
            return patient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="doctorId"></param>
        /// <returns></returns>
        public Patient AddPatient(Patient patient, int doctorId)
        {
            _patientRepository.AddPatient(patient);
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            _doctorRepository.AssignPatient(doctorId, patient.PatientId);
            return patient;
        }

        /// <summary>
        /// Assigns patient to a specific doctor
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="patientId"></param>
        public void AssignDoctorToPatient(int doctorId, int patientId)
        {
            _doctorRepository.AssignPatient(doctorId, patientId);
        }

        /// <summary>
        /// Initiates a patient referral from one doctor to another
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="referringDocId"></param>
        /// <param name="referredDocId"></param>
        /// <returns>true if patient successfully referred, else false</returns>
        public void ReferPatient(int patientId, int referringDocId, int referredDocId)
        {
            _referralRepository.AddReferral(patientId, referringDocId, referredDocId);
            AssignDoctorToPatient(referredDocId, patientId);
        }
    }
}