using System;
using System.Collections.Generic;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.PatientManagement
{
    public class PatientManagementService : IPatientManagementService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IVisitationRepository _visitationRepository;

        public PatientManagementService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
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
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            List<Patient> list = new List<Patient>(doctor.Patients);
            return list;
        }

        /// <summary>
        /// Adds Patient to specific doctor's list of patients
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="doctorId"></param>
        /// <returns>patientID if successfully added patient, else invalid value ‘-1’</returns>
        public int AddPatient(Patient patient, int doctorId)
        {
            _patientRepository.AddPatient(patient);
            Doctor doctor = _doctorRepository.GetDoctor(doctorId);
            return 1;
        }

        /// <summary>
        /// Updates a patient profile with a newly added visitation
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="visitationId"></param>
        /// <returns>true if patient updated successfully with a new visitationID, else false</returns>
        public bool AddVisitation(Patient patient, int visitationId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initiates a patient referral from one doctor to another
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="referringDocId"></param>
        /// <param name="referredDocId"></param>
        /// <returns>true if patient successfully referred, else false</returns>
        public bool ReferPatient(int patientId, int referringDocId, int referredDocId)
        {
            throw new NotImplementedException();
        }
    }
}
