using System;
using System.Collections.Generic;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.PatientManagement
{
    public class PatientManagementService : IPatientManagementService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientManagementService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Patient ViewPatient(int patientId)
        {
            return _patientRepository.GetPatient(patientId);
        }

        public List<Patient> ViewAllPatients(int doctorId)
        {
            throw new NotImplementedException();
        }

        public int AddPatient(Patient patient, int doctorId)
        {
            throw new NotImplementedException();
        }

        public bool AddVisitation(Patient patient, int visitationId)
        {
            throw new NotImplementedException();
        }

        public bool ReferPatient(int patientId, int referringDocId, int referredDocId)
        {
            throw new NotImplementedException();
        }
    }
}
