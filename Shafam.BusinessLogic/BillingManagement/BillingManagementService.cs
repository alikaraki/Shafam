using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.BillingManagement
{
    public class BillingManagementService : IBillingManagementService
    {
        private readonly IVisitationRepository _visitationRepository;
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly ITestRepository _testRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public BillingManagementService(IVisitationRepository visitationRepository, 
                                ITreatmentRepository treatmentRepository,
                                ITestRepository testRepository,
                                IDoctorRepository doctorRepository,
                                IPatientRepository patientRepository)
        {
            _visitationRepository = visitationRepository;
            _treatmentRepository = treatmentRepository;
            _testRepository = testRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public int GetNumberOfVisitationsForDoctor(int doctorId)
        {
            List<Visitation> visitations = _visitationRepository.GetVisitationsForDoctor(doctorId);
            int size = visitations.Count;
            return (size);
        }

        public List<Treatment> GetTreatmentsForDoctor(int doctorId)
        {
            throw new NotImplementedException();
        }

        public List<Test> GetTestsForDoctor(int doctorId)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfVisitationsForPatient(int patientId)
        {
            List<Visitation> visitations = _visitationRepository.GetVisitationsForPatient(patientId);
            int size = visitations.Count;
            return (size);
        }

        public List<Treatment> GetTreatmentsForPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        public List<Test> GetTestsForPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfVisitationForTime(DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }

        public List<Treatment> GetTreatmentsForTime(DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }

        public List<Test> GetTestsForTime(DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
