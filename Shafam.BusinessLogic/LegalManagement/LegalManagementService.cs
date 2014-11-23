using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.LegalService
{
    public class LegalManagementService : ILegalManagementService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IVisitationRepository _visitationRepository;

        public LegalManagementService(IDoctorRepository doctorRepository,
                            IVisitationRepository visitationRepository)
        {
            _doctorRepository = doctorRepository;
            _visitationRepository = visitationRepository;
        }

        public List<Patient> DoctorHistory(int doctorId)
        {
            return _doctorRepository.GetPatientsForDoctor(doctorId);
        }

        public List<Visitation> PatientHistory(int patientId)
        {
            return _visitationRepository.GetVisitationsForPatient(patientId);
        }
    }
}
