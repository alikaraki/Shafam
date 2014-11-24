using System.Collections.Generic;
using System.Linq;
using Shafam.Common.DataModel;
using Shafam.DataAccess.Infrastructure;

namespace Shafam.DataAccess.Repositories
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly IShafamDataContext _dataContext;

        public TreatmentRepository(IShafamDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Treatment AddTreatment(int visitationId, string treatmentType)
        {
            Visitation visitation = _dataContext.Visitations.First(v => v.VisitationId == visitationId);

            var treatment = new Treatment
                            {
                                VisitationId = visitationId,
                                TreatmentType = treatmentType,
                                DoctorId = visitation.DoctorId,
                                PatientId = visitation.PatientId
                            };

            _dataContext.Treatments.Add(treatment);
            _dataContext.Save();

            return treatment;
        }

        public List<Treatment> GetTreatmentsForPatient(int patientId)
        {
            return _dataContext.Treatments.Where(t => t.PatientId == patientId).ToList();
        }

        public List<Treatment> GetTreatmentsForVisitation(int visitationId)
        {
            return _dataContext.Treatments.Where(t => t.VisitationId == visitationId).ToList();
        }

        public void MarkAsCompleted(int treatmentId)
        {
            Treatment treatment = _dataContext.Treatments.First(t => t.TreatmentId == treatmentId);
            treatment.TreatmentCompleted = true;
            _dataContext.Save();
        }
    }
}
